﻿using QR.Billings.Business.Entities;
using QR.Billings.Business.IO.Billing;

namespace QR.Billings.Business.Interfaces.Repositories
{
    public interface IBillingRepository
    {
        Task<(IEnumerable<Billing> List, long TotalRecords)> GetPagedListByFilterAsync(BillingFilterInput filter);
        public Task<IEnumerable<Billing>> GetAll();
        Task AddAsync(Billing entity);
        Task UpdateAsync(Billing entity);
        Task<Billing> GetByIdAsync(Guid id);
        Task<IEnumerable<Billing>> GetAllUnprocessedBilling();
        Task<IEnumerable<Billing>> GetCancelledBillingsWithUncanceledTransactions();
    }
}