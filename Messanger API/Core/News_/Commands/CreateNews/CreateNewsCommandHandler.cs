using MediatR;
using Messanger_API.Core.Models;
using SocialNetwork.Interfaces;
using System.Runtime.CompilerServices;

namespace SocialNetwork.Core.News_.Commands.CreateNews
{
    public class CreateNewsCommandHandler : IRequestHandler<CreateNewsCommand, Guid>
    {
        private readonly INewsDbContext _dbContext;

        public CreateNewsCommandHandler(INewsDbContext dbContext) => 
            _dbContext = dbContext;

        public async Task<Guid> Handle(CreateNewsCommand request, CancellationToken cancellationToken)
        {
            var news = new News
            {
                UserId = request.UserId,
                Id = Guid.NewGuid(),
                Topic = request.Topic,
                MainText = request.MainText,
                ImageUrl = request.ImageUrl,
                UpdatedAt = null,
                CreatedAt = DateTime.Now
            };

            await _dbContext.News.AddAsync(news, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return news.Id;
        }
    }
}
