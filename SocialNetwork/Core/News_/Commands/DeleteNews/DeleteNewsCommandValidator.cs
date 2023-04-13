using FluentValidation;

namespace SocialNetwork.Core.News_.Commands.DeleteNews
{
    public class DeleteNewsCommandValidator : AbstractValidator<DeleteNewsCommand>
    {
        public DeleteNewsCommandValidator()
        {
            RuleFor(deleteCommand =>
                deleteCommand.UserId).NotEqual(Guid.Empty);
            RuleFor(deleteCommand =>
                deleteCommand.Id).NotEqual(Guid.Empty);
        }
    }
}
