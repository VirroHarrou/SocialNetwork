using Microsoft.EntityFrameworkCore;
using SocialNetwork.Core.Configurations;
using SocialNetwork.Core.Interfaces;
using SocialNetwork.Domain.Models;

namespace SocialNetwork.Domain.Context
{
    public class SocialNetworkContext : DbContext, INewsDbContext, ILikeDbContext, IUserDbContext
    {
        public SocialNetworkContext(DbContextOptions<SocialNetworkContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Like> Likes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new NewsConfiguration());
            builder.ApplyConfiguration(new LikeConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            base.OnModelCreating(builder);
        }
    }
}
