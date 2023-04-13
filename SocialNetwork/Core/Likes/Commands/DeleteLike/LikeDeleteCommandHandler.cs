using MediatR;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.Core.Interfaces;
using SocialNetwork.Domain.CustomExceptions;
using SocialNetwork.Domain.Models;

namespace SocialNetwork.Core.Likes.Commands.DeleteLike
{
    public class LikeDeleteCommandHandler : IRequestHandler<LikeDeleteCommand>
    {
        private readonly ILikeDbContext _context;

        public LikeDeleteCommandHandler(ILikeDbContext context) =>
            _context = context;

        public async Task Handle(LikeDeleteCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Likes
                .Where(x => x.NewsId == request.NewsId)
                .Where(x => x.UserId == request.UserId)
                .FirstOrDefaultAsync(cancellationToken);

            if (entity == null)
                throw new NotFoundException(nameof(Like), $"{request.NewsId}/{request.UserId}");

            _context.Likes.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
