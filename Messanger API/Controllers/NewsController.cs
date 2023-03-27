using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Core.News_.Commands.CreateNews;
using SocialNetwork.Core.News_.Commands.UpdateNews;
using SocialNetwork.Core.News_.Commands.DeleteNews;
using SocialNetwork.Core.News_.Queries.GetNewsDetails;
using SocialNetwork.Core.News_.Queries.GetNewsList;
using SocialNetwork.Domain.WorkModels;

namespace SocialNetwork.Controllers
{
    [Route("api/[controller]")]
    public class NewsController : BaseController
    {
        private readonly IMapper _mapper;
        public NewsController(IMapper mapper) => 
            _mapper = mapper;

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
                Id = id
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateNewsDto createNewsDto)
        {
            var command = _mapper.Map<CreateNewsCommand>(createNewsDto);
            command.UserId = UserId;
            var noteId = await Mediator.Send(command);
            return Ok(noteId);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateNewsDto updateNewsDto)
        {
            var command = _mapper.Map<UpdateNewsCommand>(updateNewsDto);
            command.UserId = UserId;
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteNewsCommand
            {
                Id = id,
                UserId = UserId
            };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
