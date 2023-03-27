using FluentValidation;

namespace SocialNetwork.Core.News_.Queries.GetNewsList
{
    public class GetNewsListQueryValidator : AbstractValidator<GetNewsListQuery>
    {
        public GetNewsListQueryValidator()
        {
            RuleFor(x => x.Count).GreaterThanOrEqualTo(1);
            RuleFor(x => x.Skip).GreaterThanOrEqualTo(0);
        }
    }
}
