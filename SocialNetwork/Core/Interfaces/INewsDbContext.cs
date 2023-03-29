using Microsoft.EntityFrameworkCore;
using SocialNetwork.Domain.Models;

namespace SocialNetwork.Core.Interfaces
{
    public interface INewsDbContext
    {
        DbSet<News> News { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
