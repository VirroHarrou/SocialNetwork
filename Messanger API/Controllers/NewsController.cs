using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Core.News_.Queries.GetNewsDetails;
using SocialNetwork.Core.News_.Queries.GetNewsList;

namespace SocialNetwork.Controllers
{
    public class NewsController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<NewsListVm>> GetAll(int count, int? skip)
        {
            var query = new GetNewsListQuery
            {
                Count = count,
                Skip = skip ??= 0
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<NewsDetailsVm>> GetDetail(Guid id)
        {
            var query = new GetNewsDetailsQuery
            {
                UserId = UserId,
                Id = id
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }
    }
}
