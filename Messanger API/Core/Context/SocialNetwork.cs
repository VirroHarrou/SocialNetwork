using Messanger_API.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Messanger_API.Core.Context
{
    public class SocialNetworkContext : DbContext
    {
        public SocialNetworkContext()
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Like> Likes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Social-Network;Integrated Security=True");
            optionsBuilder.UseSqlServer();
        }
    }
}
