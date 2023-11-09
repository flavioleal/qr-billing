using MongoDB.Driver;
using QR.Billings.Business.Entities;
using QR.Billings.Business.Enums;
using QR.Billings.Business.Interfaces.Repositories;
using QR.Billings.Business.IO.Billing;
using SharpCompress.Common;

namespace QR.Billings.Infra.Data.Repositories
{
    public class BillingRepository : IBillingRepository
    {
        private readonly IMongoCollection<Billing> _collection;

        public BillingRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<Billing>("billing");
        }

        public async Task<(IEnumerable<Billing> List, long TotalRecords)> GetPagedListByFilterAsync(BillingFilterInput filter)
        {
            var filterDefinition = Builders<Billing>.Filter.Empty;

            if (filter.Status.HasValue)
            {
                filterDefinition &= Builders<Billing>.Filter.Eq(b => b.Status, filter.Status);
            }

            if (filter.MerchantId.HasValue)
            {
                filterDefinition &= Builders<Billing>.Filter.Eq(b => b.Merchant.Id, filter.MerchantId.Value);
            }

            var query = _collection.Find(filterDefinition);

            long totalRecords = await query.CountDocumentsAsync();

            query = query.SortByDescending(b => b.CreatedAt);

            var list = await query
                                  .Skip(filter.Skip)
                                  .Limit(filter.PageSize)
                                  .ToListAsync();

            return (list, totalRecords);
        }

        public async Task<Billing> GetByIdAsync(Guid id)
        {
            return await _collection.Find(x => x.Id == id).SingleOrDefaultAsync();
        }

        public async Task AddAsync(Billing entity)
        {
            await _collection.InsertOneAsync(entity);
        }
        public async Task UpdateAsync(Billing entity)
        {
            await _collection.ReplaceOneAsync(x => x.Id == entity.Id, entity);
        }

        public async Task<IEnumerable<Billing>> GetAllUnprocessedBilling(CancellationToken cancellationToken)
        {
            return await _collection.Find(x => x.TransactionId == null).ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Billing>> GetCancelledBillingsWithUncanceledTransactions(CancellationToken cancellationToken)
        {
            return await _collection.Find(x => 
                                x.Status == PaymentStatusEnum.Canceled &&
                               x.TransactionId != null &&
                                (x.TransactionCanceled == null || x.TransactionCanceled == false) ).ToListAsync(cancellationToken);
        }
    }
}
