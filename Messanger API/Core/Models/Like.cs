using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Messanger_API.Core.Models
{
    public class Like
    {
        public Guid Id { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public Guid NewsId { get; set; }
    }
}
