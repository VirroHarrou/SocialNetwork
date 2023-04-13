using Microsoft.EntityFrameworkCore;
using SocialNetwork.Domain.Models;

namespace SocialNetwork.Core.Interfaces
{
    public interface ILikeDbContext
    {
        DbSet<Like> Likes { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
