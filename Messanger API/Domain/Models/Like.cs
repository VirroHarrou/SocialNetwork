using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialNetwork.Domain.Models
{

    public class Like
    {
        [Key]
        public Guid Id { get; set; }
        public Guid NewsId { get; set; }
        public Guid UserId { get; set; }
        public News News { get; set; }
        public User User { get; set; }
    }
}
