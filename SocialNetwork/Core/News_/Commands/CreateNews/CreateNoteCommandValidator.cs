using FluentValidation;

namespace SocialNetwork.Core.News_.Commands.CreateNews
{
    public class CreateNewsCommandValidator : AbstractValidator<CreateNewsCommand>
    {
        public CreateNewsCommandValidator()
        {
            RuleFor(createNewsCommand =>
                createNewsCommand.Topic).NotEmpty().MaximumLength(250);
            RuleFor(createNewsCommand =>
                createNewsCommand.UserId).NotEqual(Guid.Empty);
            RuleFor(createNewsCommand =>
                createNewsCommand.MainText).NotEmpty();
        }
    }
}
