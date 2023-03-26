using Messanger_API.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace SocialNetwork.Interfaces
{
    public interface ILikeDbContext
    {
        DbSet<Like> Likes { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
