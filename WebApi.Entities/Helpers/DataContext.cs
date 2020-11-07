using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebApi.Entities;

namespace WebApi.Entities.Helpers
{
    public class DataContext : DbContext
    {
        protected readonly IConfigurationRoot configuration;

        public DataContext()
        {
            configuration = new ConfigurationBuilder()
            .SetBasePath(System.AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql server database
            options.UseSqlServer(configuration.GetConnectionString("WebApiDatabase"));
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Location> Location { get; set; }
        public DbSet<Venue> Venue { get; set; }
        public DbSet<Photo> Photo { get; set; }
        public DbSet<UserLocation> UserLocation { get; set; }
    }
}
