using MentorApp.Extensions;
using MentorApp.Models;
using MentorApp.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace MentorApp.Controllers
{
    [Route("api/posts")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private const int DefaultPageSize = 10;
        private readonly MentorAppContext _context;

        public PostsController(MentorAppContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetPostList(int pageNumber = 1, int pageSize = DefaultPageSize)
        {
            return Ok(await _context.Post
                         .OrderByDescending(post => post.DateOfPublication)
                         .GetPage(pageNumber, pageSize)
                         .ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(Post post)
        {
            _context.Post.Add(post);
            await _context.SaveChangesAsync();
            return StatusCode(201, post);
        }

        [HttpGet("project/{idProject:int}")]
        public async Task<IActionResult> GetPostByProject(int idProject, int pageNumber = 1, int pageSize = DefaultPageSize)
        {
            IOrderedQueryable<Post> posts = _context.Post
                            .Where(post => post.Project.Equals(idProject))
                            .OrderByDescending(post => post.DateOfPublication);

            return Ok((await posts
                            .GetPage(pageNumber, pageSize)
                            .ToListAsync())
                            .ToPagedList(pageNumber,
                                         pageSize,
                                         await posts.CountAsync()
                            ));
        }

    }
}