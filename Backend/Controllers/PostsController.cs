﻿using System.Linq;
using System.Threading.Tasks;
using MentorApp.Extensions;
using MentorApp.Models;
using MentorApp.Wrappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MentorApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private const int DefaultPageSize = 10;
        private readonly s17874Context _context;

        public PostsController(s17874Context context)
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
            var posts = _context.Post
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