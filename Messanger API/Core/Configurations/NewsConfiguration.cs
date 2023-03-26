using Messanger_API.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SocialNetwork.Interfaces.Configurations
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
