using System.ComponentModel.DataAnnotations;

namespace Messanger_API.Core.Models
{
    public class News
    {
        [Key]
        public Guid Id { get; set; }
        public string? Topic { get; set; }
        [Required]
        public string MainText { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        public DateTime CreatedAt => DateTime.Now;
        public DateTime? UpdatedAt { get; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
