using Messanger_API.Core.Context;
using Messanger_API.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Messanger_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SocialNetworkController : Controller
    {
        private readonly ILogger _logger;
        private readonly SocialNetworkContext _context;

        public SocialNetworkController(ILogger<SocialNetworkController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _context = new SocialNetworkContext(configuration);
        }

        [HttpGet("feed")]
        public async Task<ActionResult<News>> GetNews()
        {
            using var db = _context;
            if (await db.News.CountAsync() < 1)
                return NotFound();

            var result = await db.News
                .ToListAsync();
            return new ObjectResult(result);
        }

        [HttpPost("feed")]
        public async Task<ActionResult> AddNews(News news)
        {
            if (news == null)
                return BadRequest();

            using (var db = _context)
            {
                await db.AddAsync(news);
                await db.SaveChangesAsync();
            }

            return Ok();
        }

        [HttpGet("favorite")] //Переделать метод, для вывода "лучших" новостей
        public async Task<ActionResult<News>> GetFavoriteNews()
        {
            using var db = _context;
            if (await db.News.CountAsync() < 1)
                return NotFound();

            var result = await db.News
                .ToListAsync();
            return new ObjectResult(result);
        }

        [HttpPost("registration")]
        public async Task<ActionResult> RegUser(User Newuser)
        {
            if (Newuser == null)
                return BadRequest("Is empty!");

            using (var db = _context)
            {
                var user = await db.Users
                    .Where(u => u.Id == Newuser.Id)
                    .FirstOrDefaultAsync();

                if (user != null)
                    return BadRequest("User already exists!");

                await db.AddAsync(Newuser);
                await db.SaveChangesAsync();
            }

            return Ok();
        }
    }
}
