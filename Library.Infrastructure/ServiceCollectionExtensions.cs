using Library.Infrastructure.Auth;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfraServices(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<ITokenProvider, TokenProvider>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            return services;
        }
    }
}
