using QR.Billings.Business.Entities;
using QR.Billings.Business.IO.Billing;
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
        Task CancelBillingByIdAsync(Guid id);
    }
}
