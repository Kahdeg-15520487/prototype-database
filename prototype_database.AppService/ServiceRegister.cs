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
            services.AddTransient<IUserService, UserService>();

            services.AddTransient<IOrganizationService, OrganizationService>();
            services.AddTransient<IGroupService, GroupService>();
            services.AddTransient<IRoleService, RoleService>();

            services.AddSingleton<IRandomIdGenerator, RandomIdGenerator>();

            return services;
        }
    }
}
