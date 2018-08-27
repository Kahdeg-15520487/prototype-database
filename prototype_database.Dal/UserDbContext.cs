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

        public UserDbContext(DbContextOptions options) : base(options)
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region organization
            var RosenOrg = new Organization()
            {
                Id = Guid.NewGuid(),
                Name = "Rosen"
            };
            var UITOrg = new Organization()
            {
                Id = Guid.NewGuid(),
                Name = "UIT"
            };
            Organization[] organizations = new Organization[]
            {
                RosenOrg,UITOrg
            };
            #endregion

            #region group
            var RosenTechGroup = new Group()
            {
                Id = Guid.NewGuid(),
                Name = "Technical",
                OrganizationId = RosenOrg.Id
            };
            var RosenHRGroup = new Group()
            {
                Id = Guid.NewGuid(),
                Name = "HR",
                OrganizationId = RosenOrg.Id
            };

            var UITSEGroup = new Group()
            {
                Id = Guid.NewGuid(),
                Name = "SoftwareEngineer",
                OrganizationId = UITOrg.Id
            };
            var UITCEGroup = new Group()
            {
                Id = Guid.NewGuid(),
                Name = "ComputerEngineer",
                OrganizationId = UITOrg.Id
            };

            Group[] groups = new Group[]
            {
                RosenTechGroup,RosenHRGroup,UITSEGroup,UITCEGroup
            };
            #endregion

            #region role

            var TechLeadRole = new Role
            {
                Id = Guid.NewGuid(),
                Name = "Technical Lead"
            };
            var EngineerRole = new Role
            {
                Id = Guid.NewGuid(),
                Name = "Engineer"
            };
            var HRLeadRole = new Role
            {
                Id = Guid.NewGuid(),
                Name = "HR Lead"
            };
            Role[] roles = new Role[]
            {
                TechLeadRole,HRLeadRole,EngineerRole
            };

            #endregion

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
