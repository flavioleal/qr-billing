using QR.Billings.Business.Enums;
using QR.Billings.Business.IO.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QR.Billings.Business.IO.Billing
{
    public class BillingFilterInput : PaginationFilterInput
    {
        public PaymentStatusEnum?  Status { get; set; }
        public Guid? MerchantId { get; set; }
    }
}
