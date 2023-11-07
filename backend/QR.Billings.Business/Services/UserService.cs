using QR.Billings.Business.Entities;
using QR.Billings.Business.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QR.Billings.Business.Services
{
    public class UserService : IUserService
    {
        public Task<(User, string)> AuthenticateAsync(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}
