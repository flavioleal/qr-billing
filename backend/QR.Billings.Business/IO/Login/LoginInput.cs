using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QR.Billings.Business.IO.Login
{
    public class LoginInput
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
