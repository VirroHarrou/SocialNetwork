using MediatR;

namespace SocialNetwork.Core.Likes.Commands.CreateLike
{
    public class LikeCreateCommand : IRequest
    {
        public Guid UserId { get; set; }
        public Guid NewsId { get; set; }
    }
}
