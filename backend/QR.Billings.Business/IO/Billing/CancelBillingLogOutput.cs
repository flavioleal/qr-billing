using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QR.Billings.Business.IO.Billing
{
    public class CancelBillingLogOutput
    {
        public CancelBillingLogOutput(Guid id, decimal value, Guid? cancellationUser, string cancellationUserName)
        {
            Id = id;
            Value = value;
            CancellationUser = cancellationUser;
            CancellationUserName = cancellationUserName;
        }

        public Guid Id { get; set; }
        public decimal Value { get; set; }
        public Guid? CancellationUser { get; set; }
        public string CancellationUserName { get; set; }
    }
}
