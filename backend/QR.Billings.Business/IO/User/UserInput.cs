using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QR.Billings.Business.IO.User
{
    public class UserInput
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
