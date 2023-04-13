using FluentValidation;

namespace SocialNetwork.Core.News_.Commands.UpdateNews
{
    public class UpdateNewsCommandValidator : AbstractValidator<UpdateNewsCommand>
    {
        public UpdateNewsCommandValidator()
        {
            RuleFor(updateCommand =>
                updateCommand.UserId).NotEqual(Guid.Empty);
            RuleFor(updateCommand =>
                updateCommand.Id).NotEqual(Guid.Empty);
            RuleFor(createNewsCommand =>
                createNewsCommand.Topic).NotEmpty().MaximumLength(250);
            RuleFor(createNewsCommand =>
                createNewsCommand.MainText).NotEmpty();
        }
    }
}
