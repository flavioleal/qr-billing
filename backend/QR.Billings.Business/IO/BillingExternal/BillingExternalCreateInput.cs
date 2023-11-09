using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QR.Billings.Business.IO.BillingExternal
{
    public class BillingExternalCreateInput
    {
        public BillingExternalCreateInput(decimal value)
        {
            Value = value;
        }

        public decimal Value { get; set; }
    }
}
