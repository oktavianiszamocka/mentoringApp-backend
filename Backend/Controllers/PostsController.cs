using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MentorApp.Extensions;
using MentorApp.Filter;
using MentorApp.Helpers;
using MentorApp.Models;using MentorApp.Services;
using MentorApp.Wrappers;
using MentorApp.Services;
using MentorApp.Wrappers;
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
        private readonly IUriService _uriService;

        public PostsController(s17874Context context, IUriService uriService
        {
            _context = context;
            _uriService = uriService;
        }

        //mr. Gago
/*        [HttpGet]
        public async Task<IActionResult> GetPostList(int pageNumber = 1, int pageSize = DefaultPageSize)
        {
            return Ok(await _context.Post
                         .OrderByDescending(post => post.DateOfPublication)
                         .GetPage(pageNumber, pageSize)
                         .ToListAsync());
        }*/


        //tami
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] Filter.PaginationFilter filter)
        {
            var route = Request.Path.Value;
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var pagedData = await _context.Post
                         .OrderByDescending(post => post.DateOfPublication)
                         .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                         .Take(validFilter.PageSize)
                         .ToListAsync();
            var totalRecords = await _context.Post.CountAsync();
            var pagedReponse = PaginationHelper.CreatePagedReponse<Post>(pagedData, validFilter, totalRecords, _uriService, route);
            return Ok(pagedReponse);
            //return Ok(new PagedResponse<List<Post>>(pagedData, validFilter.PageNumber, validFilter.PageSize));
        }
    

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var post = await _context.Post.Where(a => a.IdPost == id).FirstOrDefaultAsync();
            return Ok(new Response<Post>(post));
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