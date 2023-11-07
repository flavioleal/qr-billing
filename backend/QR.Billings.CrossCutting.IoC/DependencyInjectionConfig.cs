﻿using Microsoft.Extensions.DependencyInjection;
using QR.Billings.Business.Interfaces.Notifier;
using QR.Billings.Business.Interfaces.Repositories;
using QR.Billings.Business.Interfaces.Services;
using QR.Billings.Business.Interfaces.Services.Notifier;
using QR.Billings.Business.Services;
using QR.Billings.Infra.Data.Repositories;

namespace QR.Billings.CrossCutting.IoC
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {

            #region services
            services.AddScoped<INotifier, Notifier>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenService, TokenService>();
            #endregion

            #region repositories
            services.AddScoped<IUserRepository, UserRepository>();
            #endregion

            #region external services
            #endregion
        }
    }
}