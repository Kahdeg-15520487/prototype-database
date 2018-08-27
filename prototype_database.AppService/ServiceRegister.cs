using System;
using System.Linq;
using System.Collections.Generic;

using Microsoft.Extensions.DependencyInjection;

using prototype_database.AppService.Services;
using prototype_database.Contract;

namespace prototype_database.AppService
{
    public static class ServiceRegister
    {
        public static IServiceCollection AddUserService(this IServiceCollection services)
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
                        {
                            var service = serviceProvider.GetServices<IUserService>().FirstOrDefault(s => s.GetType().Equals(typeof(FullUserService)));
                            return service;
                        }
                    case ServiceType.Light:
                        {
                            var service = serviceProvider.GetServices<IUserService>().FirstOrDefault(s => s.GetType().Equals(typeof(LightUserService)));
                            return service;
                        }
                    default:
                        throw new KeyNotFoundException();
                }
            });

            services.AddSingleton<IRandomIdGenerator, RandomIdGenerator>();

            return services;
        }
    }
}
