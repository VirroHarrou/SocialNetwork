using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Messanger_API.Core.Models
{

    public class Like
    {
        [Key]
        public Guid Id { get; set; }
        public Guid NewsId { get; set; }
        public News News { get; set; }
    }
}
