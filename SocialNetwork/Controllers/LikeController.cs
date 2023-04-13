using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Core.Likes.Commands.CreateLike;
using SocialNetwork.Core.Likes.Commands.DeleteLike;
using SocialNetwork.Core.Likes.Queries.GetListLike;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace SocialNetwork.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api={version:apiVersion}/[controller]")]
    public class LikeController : BaseController
    {
        private readonly IMapper _mapper;
        public LikeController(IMapper mapper) => 
            _mapper = mapper;

        /// <summary>
        /// Gets the list of likes
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /Like/3fa85f64-5717-4562-b3fc-2c963f66afa6
        /// </remarks>
        /// <param name="newsId">The news id</param>
        /// <returns>Returns LikeListVm</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpGet("{newsId}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<LikeListVm>> GetLikes(Guid newsId)
        {
            var query = new GetListLikeQuery { NewsId = newsId };

            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        /// <summary>
        /// Like the news
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /Like/3fa85f64-5717-4562-b3fc-2c963f66afa6
        /// </remarks>
        /// <param name="newsId">The news id</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpPost("{newsId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> AddLike(Guid newsId)
        {
            var query = new LikeCreateCommand 
            {
                NewsId = newsId,
                UserId = UserId
            };

            await Mediator.Send(query);
            return NoContent();
        }

        /// <summary>
        /// Dislike the news
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE /Like/3fa85f64-5717-4562-b3fc-2c963f66afa6
        /// </remarks>
        /// <param name="newsId">The news id</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpDelete("{newsId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteLike(Guid newsId)
        {
            var query = new LikeDeleteCommand
            {
                NewsId = newsId,
                UserId = UserId
            };

            await Mediator.Send(query);
            return NoContent();
        }
    }
}
