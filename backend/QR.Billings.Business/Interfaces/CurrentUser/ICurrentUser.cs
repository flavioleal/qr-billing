using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QR.Billings.Business.Interfaces.CurrentUser
{
    public interface ICurrentUser
    {
        Guid? Id { get; }
        string Name { get; }
        string Role { get; }

    }
}
