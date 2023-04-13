using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace SocialNetwork.Domain.Models
{
    public class News
    {
        [Key]
        public Guid Id { get; set; }
        [NotNull]
        public string? Topic { get; set; }
        [NotNull]
        public string MainText { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid UserId { get; set; }
        public ICollection<Like> Likes { get; set; }
    }
}
