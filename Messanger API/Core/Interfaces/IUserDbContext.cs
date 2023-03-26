using Messanger_API.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace SocialNetwork.Interfaces
{
    public interface IUserDbContext
    {
        DbSet<User> Users { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
