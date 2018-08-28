using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;

namespace prototype_database.Dal
{
    public class UserDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
        }

        protected UserDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        /// <summary>
        /// seed data
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var seedData = SeedData.Instance;
            Organization[] organizations = new Organization[]
            {
                seedData.RosenOrg,seedData.UITOrg
            };
            Group[] groups = new Group[]
            {
                seedData.RosenTechGroup,seedData.RosenHRGroup,seedData.UITSEGroup,seedData.UITCEGroup
            };

            Role[] roles = new Role[]
            {
                seedData.TechLeadRole,seedData.HRLeadRole,seedData.EngineerRole
            };

            modelBuilder.Entity<Organization>().HasData(organizations);
            modelBuilder.Entity<Group>().HasData(groups);
            modelBuilder.Entity<Role>().HasData(roles);

            #region user
            /*
            User user = new User()
            {
                Id = "123456",
                FirstName = "First",
                LastName = "Last",
                OrganizationId = RosenOrg.Id,
                ProfileImage = "image",
                Email = "{\"main\": \"em@email.com\",\"emails\": [\"em@email.com\",\"em@yahoo.com\"]}",
                Phone = "{\"main\": \"1234\",\"work\": [\"1234\",\"5678\"], \"private\": [\"91011\"]}",
                Mobile = "{\"main\": \"333444\",\"mobiles\": [\"333444\",\"555666\"]}",
            };
            UserGroup[] userGroups = new UserGroup[]
            {
                new UserGroup()
                {
                     Id = Guid.NewGuid(),
                      UserId = "123456",
                      GroupId = RosenTechGroup.Id,
                      IsMain = true
                },
                new UserGroup()
                {
                    Id = Guid.NewGuid(),
                    UserId = "123456",
                    GroupId = RosenHRGroup.Id,
                    IsMain = false
                }
            };
            UserRole[] userRoles = new UserRole[]
            {
                new UserRole()
                {
                    Id = Guid.NewGuid(),
                    UserId = "123456",
                    RoleId=EngineerRole.Id,
                    IsMain = true
                }
            };
            modelBuilder.Entity<User>().HasData(user);
            modelBuilder.Entity<UserGroup>().HasData(userGroups);
            modelBuilder.Entity<UserRole>().HasData(userRoles);
            */
            #endregion

            base.OnModelCreating(modelBuilder);
        }
    }
}
