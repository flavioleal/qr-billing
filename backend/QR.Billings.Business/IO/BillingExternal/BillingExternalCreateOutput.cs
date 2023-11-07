using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QR.Billings.Business.IO.BillingExternal
{
    public class BillingExternalCreateOutput
    {
        public string Message { get; set; }
        public string Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string Status { get; set; }
        public string QrCode { get; set; }
    }
}
