using QR.Billings.Business.Entities;
using QR.Billings.Business.Interfaces.ExternalServices;
using QR.Billings.Business.Interfaces.Notifier;
using QR.Billings.Business.Interfaces.Services;
using QR.Billings.Business.Interfaces.Services.Base;
using QR.Billings.Business.IO.Billing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QR.Billings.Business.Services
{
    public class BillingService : BaseService, IBillingService
    {
        private readonly IBillingExternalService _billingExternalService;
        public BillingService(INotifier notifier, IBillingExternalService billingExternalService) : base(notifier)
        {
            _billingExternalService = billingExternalService;
        }

        public async Task<bool> AddAsync(AddBillingInput input)
        {
            var transaction = await _billingExternalService.Create(input.Value);

            return true;
        }

        public Task CancelBillingByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Billing>> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
