using QR.Billings.Business.Entities;
using QR.Billings.Business.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QR.Billings.Business.IO.Billing
{
    public class ListBillingOutput
    {
        public Guid Id { get; set; }
        public decimal Value { get; set; }
        public string? QrCode { get; set; }
        public DateTime CreatedAt { get; set; }
        public PaymentStatusEnum Status { get; set; }
        public string PaymentDescription { get; set; }
        public Merchant Merchant { get; set; }
        public Customer Customer { get; set; }
    }
}
