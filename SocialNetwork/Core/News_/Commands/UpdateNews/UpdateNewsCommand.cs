using MediatR;

namespace SocialNetwork.Core.News_.Commands.UpdateNews
{
    public class UpdateNewsCommand : IRequest
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Topic { get; set; }
        public string MainText { get; set; }
        public string ImageUrl { get; set; }
    }
}
