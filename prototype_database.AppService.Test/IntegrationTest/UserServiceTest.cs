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
    public class UserServiceTest : BaseServiceTest
    {
        public UserServiceTest(ITestOutputHelper output) : base(output) { }

        //generate a test user DTO
        private UserDTO GetTestUser()
        {
            SeedData seedData = SeedData.Instance;
            return new UserDTO()
            {
                FirstName = "first",
                LastName = "last",
                Organization = Mapper.Map(seedData.RosenOrg),
                MainGroup = Mapper.Map(seedData.RosenTechGroup),
                Groups = new[] { Mapper.Map(seedData.RosenTechGroup) },
                MainRole = Mapper.Map(seedData.EngineerRole),
                Roles = new[] { Mapper.Map(seedData.EngineerRole) },
                Email = new Email
                {
                    Main = "main email",
                    Emails = new[] { "main email", "not main email" }
                },
                Phone = new Phone
                {
                    Main = "main phone",
                    Work = new[] { "main phone", "work phone 2" },
                    Private = new[] { "private phone" }
                },
                Mobile = new Mobile
                {
                    Main = "mobile 1",
                    Mobiles = new[] { "mobile 1", "mobile 2" }
                }
            };
        }

        [Fact]
        public void TestCreateUser()
        {
            using (var context = InitDbContext("create_user"))
            {
                var service = new UserService(context);
                var userId = "test1";

                //create a user and insert into db
                UserDTO userDTO = GetTestUser();

                _output.WriteLine(userId);

                try
                {
                    Assert.Equal(userId, service.Create(userDTO, userId));
                }
                catch (ArgumentException aex)
                {
                    _output.WriteLine(aex.Message);
                }

                //confirm that user is saved correctly in the db
                //and userDTO is generated correctly by UserService
                //get inserted user from db
                var usetDTOfromDb = service.GetUser(userId);
                Assert.Equal(userId, usetDTOfromDb.Id);

                //confirm that user's organization is saved correctly
                Assert.Equal(userDTO.Organization.Id, usetDTOfromDb.Organization.Id);

                //confirm that user's group is saved correctly
                Assert.Equal(userDTO.MainGroup.Id, usetDTOfromDb.MainGroup.Id);
                Assert.Equal(userDTO.Groups.Length, usetDTOfromDb.Groups.Length);

                //confirm that user's role is saved correctly
                Assert.Equal(userDTO.MainRole.Id, usetDTOfromDb.MainRole.Id);
                Assert.Equal(userDTO.Roles.Length, usetDTOfromDb.Roles.Length);
            }
        }
    }
}
