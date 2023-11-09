using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QR.Billings.Business.Interfaces.ExternalServices;
using QR.Billings.Business.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QR.Billings.Business.Services
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
            var unprocessedBilling = await billingService.GetAllUnprocessedBilling(cancellationToken);

            foreach (var billing in unprocessedBilling)
            {
                var transaction = await billingExternalService.Create(billing.Value);
                if (transaction != null)
                {
                    billing.TransactionId = transaction.Id;
                    billing.QrCode = transaction.QrCode;
                    await billingService.UpdateAsync(billing);
                }
            }
        }

        private async Task CheckAndCancelBillingsWithUncanceledTransactions(CancellationToken cancellationToken, IBillingService billingService, IBillingExternalService billingExternalService)
        {
            var canceleds = await billingService.GetCancelledBillingsWithUncanceledTransactions(cancellationToken);

            foreach (var billing in canceleds)
            {
                var transaction = await billingExternalService.Cancel(billing.TransactionId);
                if (transaction != null)
                {
                    billing.TransactionCanceled = true;
                    await billingService.UpdateAsync(billing);
                }
            }
        }
    }
}
