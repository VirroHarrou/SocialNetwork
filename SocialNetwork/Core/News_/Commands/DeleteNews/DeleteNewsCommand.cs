using MediatR;

namespace SocialNetwork.Core.News_.Commands.DeleteNews
{
    public class DeleteNewsCommand : IRequest
    {
        public Guid UserId { get; set; }
        public Guid Id { get; set; }
    }
}
