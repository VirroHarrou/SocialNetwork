using Microsoft.EntityFrameworkCore;
using SocialNetwork.Core.Configurations;
using SocialNetwork.Core.Interfaces;
using SocialNetwork.Domain.Models;

namespace SocialNetwork.Domain.Context
{
    public class SocialNetworkContext : DbContext, INewsDbContext, ILikeDbContext
    {
        public SocialNetworkContext(DbContextOptions<SocialNetworkContext> options)
            : base(options) { }

        public DbSet<News> News { get; set; }
        public DbSet<Like> Likes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new NewsConfiguration());
            builder.ApplyConfiguration(new LikeConfiguration());
            base.OnModelCreating(builder);
        }
    }
}
