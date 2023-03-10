using Messanger_API.Core.Context;
using Messanger_API.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Messanger_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SocialNetworkController : Controller
    {
        private readonly ILogger _logger;

        public SocialNetworkController(ILogger<SocialNetworkController> logger)
        {
            _logger = logger;
        }

        [HttpGet("feed")]
        public async Task<ActionResult<News>> GetNews()
        {
            using (var db = new MessangerContext())
            {
                if (await db.News.CountAsync() < 1)
                    return NotFound();

                var result = await db.News
                    .ToListAsync();
                return new ObjectResult(result);
            }
        }

        [HttpPost("feed")]
        public async Task<ActionResult> AddNews(News news)
        {
            if (news == null)
                return BadRequest();

            using (var db = new MessangerContext())
            {
                await db.AddAsync(news);
                await db.SaveChangesAsync();
            }

            return Ok();
        }

        [HttpGet("favorite")] //Переделать метод, для вывода "лучших" новостей
        public async Task<ActionResult<News>> GetFavoriteNews()
        {
            using (var db = new MessangerContext())
            {
                if (await db.News.CountAsync() < 1)
                    return NotFound();

                var result = await db.News
                    .ToListAsync();
                return new ObjectResult(result);
            }
        }

        [HttpPost("registration")]
        public async Task<ActionResult> RegUser(User Newuser)
        {
            if (Newuser == null)
                return BadRequest();

            using (var db = new MessangerContext())
            {
                var user = await db.Users
                    .Where(u => u.Id == Newuser.Id)
                    .FirstOrDefaultAsync();

                if (user != null)
                    return BadRequest("User already exists");

                await db.AddAsync(Newuser);
                await db.SaveChangesAsync();
            }

            return Ok();
        }

        [HttpPost("login")]
        public async Task<ActionResult> LoginUser(User user)
        {
            return Ok();
        }
    }
}
