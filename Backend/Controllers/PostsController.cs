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


namespace MentorApp.Controllers
{
    [Route("api/posts")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private const int DefaultPageSize = 10;
        private readonly IUriService _uriService;
        private readonly IPostService _postService;
       

        public PostsController(IUriService uriService, IPostService postService)
        {
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

        [HttpGet("general")]
        public async Task<IActionResult> GetGeneral([FromQuery] Filter.PaginationFilter filter)
        {
            var route = Request.Path.Value;
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);

            var postList = await _postService.GetGeneralPost();
            var postWithPaging = postList
                               .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                               .Take(validFilter.PageSize)
                               .ToList();

            var totalRecords = postWithPaging.Count();
            var pagedReponse = PaginationHelper.CreatePagedReponse<PostWrapper>(postWithPaging, validFilter, totalRecords, _uriService, route);

            return Ok(pagedReponse);

        }

        [HttpGet("{IdPost:int}/comment")]
        public async Task<IActionResult> GetAllCommentsByPostId(int IdPost)
        {
            var postCommentList = await _postService.GetAllCommentByPostId(IdPost);
            return Ok(new Response<List<CommentWrapper>>(postCommentList));
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(Post post)
        {
            await _postService.SaveNewPost(post);
            return StatusCode(201, post);
        }
    }
}