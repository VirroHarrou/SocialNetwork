using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.News_.Queries.GetNewsList
{
    public class GetNewsListQueryHandler : IRequestHandler<GetNewsListQuery, NewsListVm>
    {
        private readonly INewsDbContext _context;
        private readonly IMapper _mapper;

        public GetNewsListQueryHandler(INewsDbContext context, IMapper mapper) =>
            (_context, _mapper) = (context, mapper);

        public async Task<NewsListVm> Handle(GetNewsListQuery request, CancellationToken cancellationToken)
        {
            var newsQuery = await _context.News
                .Where(news => news.UserId == request.UserId)
                .ProjectTo<NewsLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new NewsListVm { News = newsQuery };
        }
    }
}
