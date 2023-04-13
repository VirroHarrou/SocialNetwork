using MediatR;

namespace SocialNetwork.Core.News_.Queries.GetNewsDetails
{
    public class GetNewsDetailsQuery : IRequest<NewsDetailsVm>
    {
        public Guid Id { get; set; }
    }
}
