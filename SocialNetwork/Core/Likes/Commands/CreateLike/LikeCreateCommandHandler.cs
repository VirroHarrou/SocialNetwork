using MediatR;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.Core.Interfaces;
using SocialNetwork.Domain.Models;

namespace SocialNetwork.Core.Likes.Commands.CreateLike
{
    public class LikeCreateCommandHandler : IRequestHandler<LikeCreateCommand>
    {
        private ILikeDbContext _context;

        public LikeCreateCommandHandler(ILikeDbContext context) =>
            _context = context;

        public async Task Handle(LikeCreateCommand request, CancellationToken cancellationToken)
        {
            var checkQuery = await _context.Likes
                .Where(x => x.UserId == request.UserId)
                .Where(x => x.NewsId == request.NewsId)
                .FirstOrDefaultAsync(cancellationToken);
            if (checkQuery != null)
                return;
            var like = new Like
            {
                Id = Guid.NewGuid(),
                NewsId = request.NewsId,
                UserId = request.UserId
            };
            await _context.Likes.AddAsync(like, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
