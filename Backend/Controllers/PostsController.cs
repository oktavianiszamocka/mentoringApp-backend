using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MentorApp.Extensions;
using MentorApp.Filter;
using MentorApp.Helpers;
using MentorApp.Models;
using MentorApp.Services;
using MentorApp.Wrappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MentorApp.Persistence;
using System;
using MentorApp.Repository;

namespace MentorApp.Controllers
{
    [Route("api/posts")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private const int DefaultPageSize = 10;
        private readonly MentorAppContext _context;
        private readonly IUriService _uriService;
        private readonly IPostService _postService;
       

        public PostsController(MentorAppContext context, IUriService uriService, IPostService postService)
        {
            _context = context;
            _uriService = uriService;
            _postService = postService;
      
        }


        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] Filter.PaginationFilter filter)
        {
            var route = Request.Path.Value;
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);

            var postList = await _postService.GetAll();
            var postWithPaging = postList
               .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
               .Take(validFilter.PageSize)
               .ToList();

            var totalRecords = postWithPaging.Count();
            var pagedReponse = PaginationHelper.CreatePagedReponse<PostWrapper>(postWithPaging, validFilter, totalRecords, _uriService, route);
            
            return Ok(pagedReponse);
          
        }

        [HttpGet("project/{idProject:int}")]
        public async Task<IActionResult> GetPostByProject([FromQuery] Filter.PaginationFilter filter, int idProject)
        {
            var route = Request.Path.Value;
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);

            var postList = await _postService.GetPostProject(idProject);
            var postWithPaging =  postList
               .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
               .Take(validFilter.PageSize)
               .ToList();

            var totalRecords = postWithPaging.Count();
            var pagedReponse = PaginationHelper.CreatePagedReponse<PostWrapper>(postWithPaging, validFilter, totalRecords, _uriService, route);

            return Ok(pagedReponse);

        }
       

        [HttpGet("{idPost:int}/comment")]
        public async Task<IActionResult> GetAllCommentsByPostId(int idPost)
        {
            var postCommentList = await _context.Post
                .Where(post => post.IdPost.Equals(idPost))
                .Include(post => post.Comment)
                     .ThenInclude(comment => comment.CreatedByNavigation)
                
                .Select(post => post.Comment.OrderBy(comment => comment.CreatedOn).Select(comment => new CommentWrapper
                {

                    IdComment = comment.IdComment,
                    Comment = comment.Comment1,
                    CreatedOn = comment.CreatedOn,
                    CreatedBy = new UserWrapper
                    {
                        firstName = comment.CreatedByNavigation.FirstName,
                        lastName = comment.CreatedByNavigation.LastName,
                        imageUrl = comment.CreatedByNavigation.Avatar
                    }
                })
                .ToList()) 
                .FirstOrDefaultAsync();
               
            return Ok(new Response<List<CommentWrapper>>(postCommentList));
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(Post post)
        {
            _context.Post.Add(post);
            await _context.SaveChangesAsync();
            return StatusCode(201, post);
        }
    }
}