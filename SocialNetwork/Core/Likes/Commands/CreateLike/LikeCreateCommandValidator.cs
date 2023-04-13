using FluentValidation;

namespace SocialNetwork.Core.Likes.Commands.CreateLike
{
    public class LikeCreateCommandValidator : AbstractValidator<LikeCreateCommand>
    {
        public LikeCreateCommandValidator()
        {
            RuleFor(x => x.UserId).NotEqual(Guid.Empty);
            RuleFor(x => x.NewsId).NotEqual(Guid.Empty);
        }
    }
}
