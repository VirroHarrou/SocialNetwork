using Microsoft.EntityFrameworkCore;
using SocialNetwork.Domain.Models;
using SocialNetwork.Core.Interfaces;
using SocialNetwork.Interfaces.Configurations;

namespace SocialNetwork.Domain.Context
{
    public class SocialNetworkContext : DbContext, INewsDbContext, ILikeDbContext, IUserDbContext
    {
        //public IConfiguration AppConfiguration { get; }

        //public SocialNetworkContext(IConfiguration configuration) =>
        //    AppConfiguration = configuration;

        public SocialNetworkContext(DbContextOptions<SocialNetworkContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Like> Likes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //var ConnectionString = AppConfiguration.GetConnectionString("DebugConnection");

            //optionsBuilder.UseSqlServer(ConnectionString);

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new NewsConfiguration());
            builder.ApplyConfiguration(new LikeConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            base.OnModelCreating(builder);
        }
    }
}
