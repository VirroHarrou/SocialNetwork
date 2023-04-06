using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.Core.Interfaces;

namespace SocialNetwork.Core.Likes.Queries.GetListLike
{
    public class GetLikeListQueryHandler : IRequestHandler<GetListLikeQuery, LikeListVm>
    {
        private readonly ILikeDbContext _context;
        private readonly IMapper _mapper;

        public GetLikeListQueryHandler(ILikeDbContext context, IMapper mapper) =>
            (_context, _mapper) = (context, mapper);

        public async Task<LikeListVm> Handle(GetListLikeQuery request, CancellationToken cancellationToken)
        {
            var likeList = await _context.Likes
                .Where(x => x.NewsId == request.NewsId)
                .ProjectTo<LikeLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return new LikeListVm { Likes = likeList };
        }
    }
}
