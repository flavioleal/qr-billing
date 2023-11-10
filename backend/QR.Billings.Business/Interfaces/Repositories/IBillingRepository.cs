using QR.Billings.Business.Entities;
using QR.Billings.Business.IO.Billing;

namespace QR.Billings.Business.Interfaces.Repositories
{
    public interface IBillingRepository
    {
        Task<(IEnumerable<Billing> List, long TotalRecords)> GetPagedListByFilterAsync(BillingFilterInput filter);
        Task AddAsync(Billing entity);
        Task UpdateAsync(Billing entity);
        Task<Billing> GetByIdAsync(Guid id);
        Task<IEnumerable<Billing>> GetAllUnprocessedBilling(CancellationToken cancellationToken);
        Task<IEnumerable<Billing>> GetCancelledBillingsWithUncanceledTransactions(CancellationToken cancellationToken);
    }
}
