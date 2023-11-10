using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QR.Billings.Business.Interfaces.ExternalServices;
using QR.Billings.Business.Interfaces.Services;
using QR.Billings.Business.IO.BillingExternal;
using Serilog;

namespace QR.Billings.Business.Services.Worker
{
    public class BillingTransactionBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public BillingTransactionBackgroundService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var billingService = scope.ServiceProvider.GetRequiredService<IBillingService>();
                var billingExternalService = scope.ServiceProvider.GetRequiredService<IBillingExternalService>();

                Log.Information("Worker insider API running running at: {time}", DateTimeOffset.Now);

                while (!stoppingToken.IsCancellationRequested)
                {
                    await CheckAndGenerateUnprocessedBillings(stoppingToken, billingService, billingExternalService);
                    await CheckAndCancelBillingsWithUncanceledTransactions(stoppingToken, billingService, billingExternalService);
                    await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
                }
            }
        }

        private async Task CheckAndGenerateUnprocessedBillings(CancellationToken cancellationToken, IBillingService billingService, IBillingExternalService billingExternalService)
        {
            var billingCreateExternalServiceLog = new CreateBillingExternalServiceLogOutput();
            try
            {
                var unprocessedBilling = await billingService.GetAllUnprocessedBilling(cancellationToken);

                if (unprocessedBilling != null && unprocessedBilling.Count() > 0)
                {
                    billingCreateExternalServiceLog.Total = unprocessedBilling.Count();
                    foreach (var billing in unprocessedBilling)
                    {
                        var transaction = await billingExternalService.Create(billing.Value);
                        if (transaction != null)
                        {
                            billing.TransactionId = transaction.Id;
                            billing.QrCode = transaction.QrCode;
                            await billingService.UpdateAsync(billing);
                            billingCreateExternalServiceLog.TotalProcessed++;
                        }
                    }
                    Log.Information($"[Create billing external] - Success  {{@billingCreateExternalServiceLog}}", billingCreateExternalServiceLog);
                }


            }
            catch (Exception ex)
            {
                Log.Error($"[Create billing external] - Error {{@billingCreateExternalServiceLog}}", billingCreateExternalServiceLog, ex);
                throw;
            }
        }

        private async Task CheckAndCancelBillingsWithUncanceledTransactions(CancellationToken cancellationToken, IBillingService billingService, IBillingExternalService billingExternalService)
        {
            var billingCancelExternalServiceLog = new CancelBillingExternalServiceLogOutput();
            try
            {
                var canceleds = await billingService.GetCancelledBillingsWithUncanceledTransactions(cancellationToken);

                if (canceleds != null && canceleds.Count() > 0)
                {
                    billingCancelExternalServiceLog.Total = canceleds.Count();
                    foreach (var billing in canceleds)
                    {
                        var transaction = await billingExternalService.Cancel(billing.TransactionId);
                        if (transaction != null)
                        {
                            billing.TransactionCanceled = true;
                            await billingService.UpdateAsync(billing);
                            billingCancelExternalServiceLog.TotalProcessed++;
                        }
                    }
                    Log.Information($"[Cancel billing external] - Success  {{@billingCancelExternalServiceLog}}", billingCancelExternalServiceLog);
                }
            }
            catch (Exception ex)
            {
                Log.Error($"[Cancel billing external] - Error {{@billingCancelExternalServiceLog}}", billingCancelExternalServiceLog, ex);
                throw;
            }
        }
    }
}
