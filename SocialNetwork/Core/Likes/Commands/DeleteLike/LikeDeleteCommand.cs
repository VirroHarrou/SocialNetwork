using MediatR;

namespace SocialNetwork.Core.Likes.Commands.DeleteLike
{
    public class LikeDeleteCommand : IRequest
    {
        public Guid UserId { get; set; }
        public Guid NewsId { get; set; }
    }
}
