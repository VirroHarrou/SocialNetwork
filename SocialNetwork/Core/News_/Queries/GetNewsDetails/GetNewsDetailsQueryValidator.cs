using FluentValidation;

namespace SocialNetwork.Core.News_.Queries.GetNewsDetails
{
    public class GetNewsDetailsQueryValidator : AbstractValidator<GetNewsDetailsQuery>
    {
        public GetNewsDetailsQueryValidator()
        {
            RuleFor(x =>
                x.Id).NotEqual(Guid.Empty);
        }
    }
}
