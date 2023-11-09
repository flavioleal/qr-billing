﻿using QR.Billings.Business.BusinessObjects;
using QR.Billings.Business.Entities;
using QR.Billings.Business.Enums;
using QR.Billings.Business.Interfaces.CurrentUser;
using QR.Billings.Business.Interfaces.ExternalServices;
using QR.Billings.Business.Interfaces.Notifier;
using QR.Billings.Business.Interfaces.Repositories;
using QR.Billings.Business.Interfaces.Services;
using QR.Billings.Business.Interfaces.Services.Base;
using QR.Billings.Business.IO.Billing;
using QR.Billings.Business.IO.Common;
using QR.Billings.Business.Utils;

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

        public async Task<bool> AddAsync(AddBillingInput input)
        {
            if (!ExecuteValidation(new AddBillingValidation(), input)) return false;

            var billing = new Billing(input.Value);
            billing.PrepareDataForAddition(input, _currentUser);

            await _billingRepository.AddAsync(billing);

            return true;
        }

        public async Task<bool> CancelBillingByIdAsync(Guid id)
        {
            var billing = await _billingRepository.GetByIdAsync(id);
            if (billing == null)
            {
                Notify("Cobrança não encontrada!");
                return false;
            }

            billing.Cancel(_currentUser.Id.Value);

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

        public async Task<IEnumerable<Billing>> GetCancelledBillingsWithUncanceledTransactions()
        {
            return await _billingRepository.GetCancelledBillingsWithUncanceledTransactions();
        }

        public async Task<Pagination<ListBillingOutput>> GetPagedListByFilterAsync(BillingFilterInput filter)
        {
            var (list, totalRecords) = await _billingRepository.GetPagedListByFilterAsync(filter);

            var projectedList = list.Select(x => new ListBillingOutput
            {
                Id = x.Id,
                Value = x.Value,
                QrCode = x.QrCode,
                CreatedAt = x.CreatedAt,
                Status = x.Status,
                PaymentDescription = EnumUtils.GetDescriptionFromEnumValue(x.Status),
                Customer = new Customer(x.Customer.Id, x.Customer.Name, x.Customer.Email)

            });

            return new Pagination<ListBillingOutput>()
            {
                List = projectedList,
                TotalRecords = totalRecords
            };
        }

        public async Task UpdateAsync(Billing billing)
        {
            await _billingRepository.UpdateAsync(billing);
        }
    }
}