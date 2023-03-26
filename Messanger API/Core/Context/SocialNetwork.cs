using Messanger_API.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Messanger_API.Core.Context
{
    public class SocialNetworkContext : DbContext
    {
        public IConfiguration AppConfiguration { get; }

        public SocialNetworkContext(IConfiguration configuration) =>
            AppConfiguration = configuration;

        public DbSet<User> Users { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Like> Likes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var ConnectionString = AppConfiguration.GetConnectionString("DebugConnection");

            optionsBuilder.UseSqlServer(ConnectionString);
        }
    }
}
