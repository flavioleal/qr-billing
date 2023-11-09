using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QR.Billings.Business.IO.BillingExternal
{
    public class BillingExternalCancelInput
    {
        public BillingExternalCancelInput(string id)
        {
            Id = id;
        }

        public string Id { get; set; }
    }
}
