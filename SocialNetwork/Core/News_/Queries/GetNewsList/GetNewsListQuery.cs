using MediatR;

namespace SocialNetwork.Core.News_.Queries.GetNewsList
{
    public class GetNewsListQuery : IRequest<NewsListVm>
    {
        public int Count { get; set; }
        public int Skip { get; set; }
    }
}
