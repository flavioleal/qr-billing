using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QR.Billings.Business.Enums
{
    public enum PaymentStatusEnum
    {
        [Description("Pendente")]
        Pending = 1,
        [Description("Cancelada")]
        Canceled = 2,
        [Description("Paga")]
        Paid = 3
    }
}
