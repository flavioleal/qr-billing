using QR.Billings.Business.Entities;
using QR.Billings.Business.IO.Billing;
using QR.Billings.Business.IO.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QR.Billings.Business.Interfaces.Services
{
    public interface IBillingService
    {
        Task<IEnumerable<Billing>> GetAll();
        Task<bool> AddAsync(AddBillingInput input);
        Task<bool> CancelBillingByIdAsync(Guid id);
        Task UpdateAsync(Billing billing);
        Task<Pagination<ListBillingOutput>> GetPagedListByFilterAsync(BillingFilterInput filter);
        Task<IEnumerable<Billing>> GetAllUnprocessedBilling(CancellationToken cancellationToken);
        Task<IEnumerable<Billing>> GetCancelledBillingsWithUncanceledTransactions(CancellationToken cancellationToken);
    }
}
