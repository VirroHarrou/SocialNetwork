using FluentValidation;

namespace SocialNetwork.Core.Likes.Commands.DeleteLike
{
    public class LikeDeleteCommandValidator : AbstractValidator<LikeDeleteCommand>
    {
        public LikeDeleteCommandValidator()
        {
            RuleFor(x => x.UserId).NotEqual(Guid.Empty);
            RuleFor(x => x.NewsId).NotEqual(Guid.Empty);
        }
    }
}
