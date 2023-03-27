using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.Core.Interfaces;
using SocialNetwork.Domain.CustomExceptions;
using SocialNetwork.Domain.Models;

namespace SocialNetwork.Core.News_.Queries.GetNewsDetails
{
    public class GetNewsDetailsQueryHandler : IRequestHandler<GetNewsDetailsQuery, NewsDetailsVm>
    {
        private readonly INewsDbContext _context;
        private readonly IMapper _mapper;

        public GetNewsDetailsQueryHandler(INewsDbContext context, IMapper mapper) =>
            (_context, _mapper) = (context, mapper);
        public async Task<NewsDetailsVm> Handle(GetNewsDetailsQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.News
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity == null || entity.UserId != request.UserId)
            {
                throw new NotFoundException(nameof(News), request.Id);
            }

            return _mapper.Map<NewsDetailsVm>(entity);
        }
    }
}
