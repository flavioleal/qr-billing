using QR.Billings.Business.Entities;
using QR.Billings.Business.Enums;
using QR.Billings.Business.Interfaces.ExternalServices;
using QR.Billings.Business.Interfaces.Notifier;
using QR.Billings.Business.Interfaces.Repositories;
using QR.Billings.Business.Interfaces.Services;
using QR.Billings.Business.Interfaces.Services.Base;
using QR.Billings.Business.IO.Billing;
using QR.Billings.Business.IO.Common;

namespace QR.Billings.Business.Services
{
    public class BillingService : BaseService, IBillingService
    {
        private readonly IBillingExternalService _billingExternalService;
        private readonly IBillingRepository _billingRepository;
        public BillingService(INotifier notifier, IBillingExternalService billingExternalService, IBillingRepository billingRepository) : base(notifier)
        {
            _billingExternalService = billingExternalService;
            _billingRepository = billingRepository;
        }

        public async Task<bool> AddAsync(AddBillingInput input)
        {

            var billing = new Billing();
            billing.Value = input.Value;
            billing.Status = PaymentStatusEnum.Pending;
            billing.Customer = new Customer(input.Customer.Name, input.Customer.Email);
            billing.Merchant = new Merchant { Id = Guid.Parse("6466a9b7-3f3d-4cbb-8e8a-59a5f109d50f"), Name = "Lojista 1" };

            await _billingRepository.AddAsync(billing);

            return true;
        }

        public async Task<bool> CancelBillingByIdAsync(Guid id)
        {
            var billing = await _billingRepository.GetByIdAsync(id);
            if(billing == null)
            {
                Notify("Cobrança não encontrada!");
                return false;
            }

            billing.Status = PaymentStatusEnum.Canceled;

            await _billingRepository.UpdateAsync(billing);

            return true;
        }

        public async Task<IEnumerable<Billing>> GetAll()
        {
            return await _billingRepository.GetAll();
        }

        public async Task<IEnumerable<Billing>> GetAllUnprocessedBilling()
        {
            return await _billingRepository.GetAllUnprocessedBilling();
        }

        public async Task<Pagination<Billing>> GetPagedListByFilterAsync(BillingFilterInput filter)
        {
            var (list, totalRecords) =  await _billingRepository.GetPagedListByFilterAsync(filter);

            return new Pagination<Billing>()
            {
                List = list,
                TotalRecords = totalRecords
            }; 
        }

        public async Task UpdateAsync(Billing billing)
        {
            await _billingRepository.UpdateAsync(billing);
        }
    }
}
