﻿using QR.Billings.Business.Entities;
using QR.Billings.Business.IO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QR.Billings.Business.Interfaces.Services
{
    public interface IUserService
    {
        Task<(User?, string)> AuthenticateAsync(UserInput input);
    }
}