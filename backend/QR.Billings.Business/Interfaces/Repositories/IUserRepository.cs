using QR.Billings.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QR.Billings.Business.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetAsync(string username, string password);
    }
}
