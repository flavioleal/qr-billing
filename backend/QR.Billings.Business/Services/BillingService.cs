using QR.Billings.Business.Entities;
using QR.Billings.Business.Interfaces.CurrentUser;
using QR.Billings.Business.Interfaces.Notifier;
using QR.Billings.Business.Interfaces.Repositories;
using QR.Billings.Business.Interfaces.Services;
using QR.Billings.Business.Interfaces.Services.Base;
using QR.Billings.Business.IO.Billing;
using QR.Billings.Business.IO.Common;
using QR.Billings.Business.Utils;
using Serilog;

namespace QR.Billings.Business.Services
{
    public class BillingService : BaseService, IBillingService
    {
        private readonly IBillingRepository _billingRepository;
        private readonly ICurrentUser _currentUser;
        public BillingService(INotifier notifier, IBillingRepository billingRepository, ICurrentUser currentUser) : base(notifier)
        {
            _billingRepository = billingRepository;
            _currentUser = currentUser;
        }

        public async Task<Pagination<ListBillingOutput>> GetPagedListByFilterAsync(BillingFilterInput filter)
        {
            if (_currentUser.Role == "lojista")
            {
                filter.MerchantId = _currentUser.Id;
            }

            var (list, totalRecords) = await _billingRepository.GetPagedListByFilterAsync(filter);

            var projectedList = list.Select(x => new ListBillingOutput
            {
                Id = x.Id,
                Value = x.Value,
                QrCode = x.QrCode,
                CreatedAt = x.CreatedAt,
                Status = x.Status,
                PaymentDescription = EnumUtils.GetDescriptionFromEnumValue(x.Status),
                Customer = new Customer(x.Customer.Id, x.Customer.Name, x.Customer.Email),
                Merchant = new Merchant(x.Merchant.Id, x.Merchant.Name)
            });

            return new Pagination<ListBillingOutput>()
            {
                List = projectedList,
                TotalRecords = totalRecords
            };
        }

        public async Task<bool> AddAsync(AddBillingInput input)
        {
            if (!ExecuteValidation(new AddBillingValidation(), input)) return false;

            var billing = new Billing(input.Value);
            billing.PrepareDataForAddition(input, _currentUser);

            var addBillingLog = new AddBillingLogOutput(billing.Id, billing.Value, billing.Merchant.Id, billing.Merchant.Name);
            try
            {
                await _billingRepository.AddAsync(billing);

                Log.Information($"[Add billing] - Success {{@addBillingLog}}", addBillingLog);
                return true;
            }
            catch (Exception ex)
            {
                Log.Error($"[Add billing] - Error {{@addBillingLog}}", addBillingLog, ex);
                throw;
            }
        }

        public async Task<bool> CancelBillingByIdAsync(Guid id)
        {
            var billing = await _billingRepository.GetByIdAsync(id);
            if (billing == null)
            {
                Notify("Cobrança não encontrada!");
                return false;
            }
            if (_currentUser.Id == null)
            {
                Notify("Ususário não está logado!");
                return false;
            }

            billing.Cancel(_currentUser.Id.Value);

            var cancelBillingLog = new CancelBillingLogOutput(billing.Id, billing.Value, _currentUser.Id, _currentUser.Name);

            try
            {
                await UpdateAsync(billing);

                Log.Information($"[Cancel billing] - Success {{@cancelBillingLog}}", cancelBillingLog);
                return true;
            }
            catch (Exception ex)
            {
                Log.Error($"[Cancel billing] - Error {{@cancelBillingLog}}", cancelBillingLog, ex);
                throw;
            }
        }

        public async Task UpdateAsync(Billing billing)
        {
            await _billingRepository.UpdateAsync(billing);
        }

        public async Task<IEnumerable<Billing>> GetAllUnprocessedBilling(CancellationToken cancellationToken)
        {
            return await _billingRepository.GetAllUnprocessedBilling(cancellationToken);
        }

        public async Task<IEnumerable<Billing>> GetCancelledBillingsWithUncanceledTransactions(CancellationToken cancellationToken)
        {
            return await _billingRepository.GetCancelledBillingsWithUncanceledTransactions(cancellationToken);
        }
    }
}
