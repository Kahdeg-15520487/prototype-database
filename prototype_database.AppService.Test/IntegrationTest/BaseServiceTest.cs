using Microsoft.EntityFrameworkCore;
using prototype_database.Dal;
using System;
using System.Collections.Generic;
using System.Text;

using Xunit;
using Xunit.Abstractions;

namespace prototype_database.AppService.Test.IntegrationTest
{
    public class BaseServiceTest
    {
        protected readonly ITestOutputHelper _output;

        public BaseServiceTest(ITestOutputHelper output)
        {
            _output = output;
        }

        protected UserDbContext InitDbContext(string dbName = "")
        {
            var option = new DbContextOptionsBuilder<UserDbContext>()
                        .UseInMemoryDatabase(databaseName: dbName)
                        .Options;

            var context = new UserDbContext(option);

            //ensure data is seeded in inmem db
            context.Database.EnsureCreated();

            return context;
        }
    }
}
