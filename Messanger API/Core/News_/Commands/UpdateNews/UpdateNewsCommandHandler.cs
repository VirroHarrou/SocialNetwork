using MediatR;
using Messanger_API.Core.Models;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.Core.News_.Commands.CreateNews.UpdateNews;
using SocialNetwork.Domain.CustomExceptions;
using SocialNetwork.Interfaces;

namespace SocialNetwork.Core.News_.Commands.UpdateNews
{
    public class UpdateNewsCommandHandler : IRequestHandler<UpdateNewsCommand>
    {
        private readonly INewsDbContext _context;

        public UpdateNewsCommandHandler(INewsDbContext context) =>
            _context = context;

        public async Task Handle(UpdateNewsCommand request, CancellationToken cancellationToken) //Возможно надо будет сделать через Unit
        {
            var entity = await _context.News.FirstOrDefaultAsync(n =>
            n.Id == request.Id, cancellationToken);
            if (entity == null || entity.UserId != request.UserId)
            {
                throw new NotFoundException(nameof(News), request.Id);
            }

            entity.MainText = request.MainText;
            entity.Topic = request.Topic;
            entity.ImageUrl = request.ImageUrl;
            entity.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
