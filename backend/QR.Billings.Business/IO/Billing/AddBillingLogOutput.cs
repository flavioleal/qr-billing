using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QR.Billings.Business.IO.Billing
{
    public class AddBillingLogOutput
    {
        public AddBillingLogOutput(Guid id, decimal value, Guid merchantId, string merchantName)
        {
            Id = id;
            Value = value;
            MerchantId = merchantId;
            MerchantName = merchantName;
        }

        public Guid Id { get; set; }
        public decimal Value { get; set; }
        public Guid MerchantId { get; set; }
        public string MerchantName { get; set; }
    }
}
