using MediatR;
using SocialNetwork.Core.Interfaces;
using SocialNetwork.Domain.CustomExceptions;
using SocialNetwork.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                .FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity == null || entity.UserId != request.UserId || entity.NewsId != request.NewsId)
                throw new NotFoundException(nameof(Like), request.Id);

            _context.Likes.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
