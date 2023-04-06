using MediatR;

namespace SocialNetwork.Core.Likes.Queries.GetListLike
{
    public class GetListLikeQuery : IRequest<LikeListVm>
    {
        public Guid NewsId { get; set; }
    }
}
