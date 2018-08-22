using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
    }
}
