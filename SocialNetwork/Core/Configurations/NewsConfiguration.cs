using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.Domain.Models;

namespace SocialNetwork.Core.Configurations
{
    public class NewsConfiguration : IEntityTypeConfiguration<News>
    {
        public void Configure(EntityTypeBuilder<News> builder)
        {
            builder.HasKey(news => news.Id);
            builder.HasIndex(news => news.Id).IsUnique();
            builder.Property(News => News.Topic).HasMaxLength(250);
        }
    }
}
