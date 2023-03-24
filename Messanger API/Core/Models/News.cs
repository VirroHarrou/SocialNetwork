using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Messanger_API.Core.Models
{
    public class News
    {
        [Key]
        public Guid Id { get; set; }
        public string? Topic { get; set; }
        [NotNull]
        public string MainText { get; set; }
        [NotNull]
        public string ImageUrl { get; set; }
        public DateTime CreatedAt => DateTime.Now;
        public DateTime? UpdatedAt { get; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public ICollection<Like> Likes { get; set; }
    }
}
