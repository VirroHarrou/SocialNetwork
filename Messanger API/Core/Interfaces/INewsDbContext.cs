using Messanger_API.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace SocialNetwork.Interfaces
{
    public interface INewsDbContext
    {
        DbSet<News> News { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
