using System;
using System.Collections.Generic;

using Microsoft.Extensions.DependencyInjection;

using prototype_database.AppService.Services;
using prototype_database.Contract;

namespace prototype_database.AppService
{
    public static class ServiceRegister
    {
        public static IServiceCollection RegisterUserService(this IServiceCollection services)
        {
            services.AddTransient<IUserService, FullUserService>();
            services.AddTransient<IUserService, LightUserService>();

            services.AddTransient<IOrganizationService, OrganizationService>();
            services.AddTransient<IGroupService, GroupService>();
            services.AddTransient<IRoleService, RoleService>();

            services.AddTransient<Func<ServiceType, IUserService>>(serviceProvider => key =>
            {
                switch (key)
                {
                    case ServiceType.Full:
                        return serviceProvider.GetService<FullUserService>();
                    case ServiceType.Light:
                        return serviceProvider.GetService<LightUserService>();
                    default:
                        throw new KeyNotFoundException();
                }
            });

            services.AddSingleton<IRandomIdGenerator, RandomIdGenerator>();

            return services;
        }
    }
}
