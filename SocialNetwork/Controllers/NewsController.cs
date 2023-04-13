using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Core.News_.Commands.CreateNews;
using SocialNetwork.Core.News_.Commands.UpdateNews;
using SocialNetwork.Core.News_.Commands.DeleteNews;
using SocialNetwork.Core.News_.Queries.GetNewsDetails;
using SocialNetwork.Core.News_.Queries.GetNewsList;
using SocialNetwork.Domain.WorkModels;
using Microsoft.AspNetCore.Authorization;

namespace SocialNetwork.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api={version:apiVersion}/[controller]")]
    public class NewsController : BaseController
    {
        private readonly IMapper _mapper;
        public NewsController(IMapper mapper) => 
            _mapper = mapper;

        /// <summary>
        /// Gets the list of news
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /news
        /// </remarks>
        /// <param name="count">The count of news items in the response</param>
        /// <param name="skip">Number of skiped news</param>
        /// <returns>Returns NewsListVm</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpGet("List/{count}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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

        /// <summary>
        /// Gets the news by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /news/3fa85f64-5717-4562-b3fc-2c963f66afa6
        /// </remarks>
        /// <param name="id">News id (Guid)</param>
        /// <returns>Returns NewsDetailsVm</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<NewsDetailsVm>> GetDetail(Guid id)
        {
            var query = new GetNewsDetailsQuery
            {
                Id = id
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        /// <summary>
        /// Creates the news
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /news
        /// {
        ///     topic: "news title",
        ///     mainText: "news description",
        ///     imageUrl: "link to the image"
        /// }
        /// </remarks>
        /// <param name="createNewsDto">CreateNewsDto object</param>
        /// <returns>Returns id (Guid)</returns>
        /// <response code="201">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateNewsDto createNewsDto)
        {
            var command = _mapper.Map<CreateNewsCommand>(createNewsDto);
            command.UserId = UserId;
            var noteId = await Mediator.Send(command);
            return Ok(noteId);
        }

        /// <summary>
        /// Updates the news
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// PUT /news
        /// {
        ///     id: "id news"
        ///     topic: "updated news title",
        ///     mainText: "updated news description"
        ///     imageUrl: "updated news image url"
        /// }
        /// </remarks>
        /// <param name="updateNewsDto">UpdateNewsDto object</param>
        /// <returns>Returns NoContent</returns>
        /// <responce code="204">Success</responce>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPut]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Update([FromBody] UpdateNewsDto updateNewsDto)
        {
            var command = _mapper.Map<UpdateNewsCommand>(updateNewsDto);
            command.UserId = UserId;
            await Mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Deletes the news by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE /news/3fa85f64-5717-4562-b3fc-2c963f66afa6
        /// </remarks>
        /// <param name="id">News id (Guid)</param>
        /// <returns>Returns NoContent</returns>
        /// <responce code="204">Success</responce>
        /// <response code="401">If the user is unauthorized</response>
        [HttpDelete("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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
