using FluentValidation;

namespace SocialNetwork.Core.Likes.Queries.GetListLike
{
    public class GetListLikeQueryValidator : AbstractValidator<GetListLikeQuery>
    {
        public GetListLikeQueryValidator()
        {
            RuleFor(x => x.NewsId).NotEqual(Guid.Empty);
        }
    }
}
