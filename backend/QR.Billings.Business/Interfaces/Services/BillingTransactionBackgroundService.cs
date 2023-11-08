using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QR.Billings.Business.Interfaces.ExternalServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QR.Billings.Business.Interfaces.Services
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
            while (!stoppingToken.IsCancellationRequested)
            {
                await CheckAndGenerateUnprocessedBillings();
                await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
            }
        }

        private async Task CheckAndGenerateUnprocessedBillings()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var billingService = scope.ServiceProvider.GetRequiredService<IBillingService>();
                var billingExternalService = scope.ServiceProvider.GetRequiredService<IBillingExternalService>();

                var unprocessedBilling = await billingService.GetAllUnprocessedBilling();

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
        }
    }
}
