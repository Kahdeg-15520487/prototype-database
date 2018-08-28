using System;
using System.Linq;
using System.Collections.Generic;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

using Xunit;

using prototype_database.AppService;
using prototype_database.AppService.Services;
using prototype_database.AppService.Utility;
using prototype_database.Contract.DTOs;
using prototype_database.Dal;
using Xunit.Abstractions;

namespace prototype_database.AppService.Test.IntegrationTest
{
    public class MiscellaneousServiceTest : BaseServiceTest
    {
        public MiscellaneousServiceTest(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void TestGetAllRole()
        {
            using (var context = InitDbContext("get_all_roles"))
            {
                //instantiate a role service
                var service = new RoleService(context);

                //test get all roles
                Assert.Equal(3, service.GetRoles().Count());
            }
        }

        [Fact]
        public void TestGetOneRole()
        {
            using (var context = InitDbContext("get_one_role"))
            {
                //instantiate a role service
                var service = new RoleService(context);

                //get engineer role
                var role = service.GetRole(Guid.Parse("d1eb257f-9a58-4751-8a6d-a1f0ed91b3ba"));

                //test get all roles
                Assert.Equal("Engineer", role.Name);
            }
        }
    }
}
