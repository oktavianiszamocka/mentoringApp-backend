using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MentorApp.DTOs.Requests;
using MentorApp.Filter;
using MentorApp.Helpers;
using MentorApp.Models;
using MentorApp.Services;
using MentorApp.Wrappers;
using Microsoft.AspNetCore.Mvc;

namespace MentorApp.Controllers
{
    [Route("api/posts")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private const int DefaultPageSize = 10;
        private readonly IPostService _postService;
        private readonly IUriService _uriService;


        public PostsController(IUriService uriService, IPostService postService)
        {
            _uriService = uriService;
            _postService = postService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);

            var postList = await _postService.GetAll();
            var postWithPaging = postList
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToList();

            var totalRecords = postList.Count();

            var pagedReponse =
                PaginationHelper.CreatePagedReponse(postWithPaging, validFilter, totalRecords, _uriService, route);

            return Ok(pagedReponse);
        }

        [HttpGet("project/{idProject:int}")]
        public async Task<IActionResult> GetPostByProject([FromQuery] PaginationFilter filter, int idProject)
        {
            var route = Request.Path.Value;
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);

            var postList = await _postService.GetPostProject(idProject);
            var postWithPaging = postList
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToList();

            var totalRecords = postList.Count();
            var pagedReponse =
                PaginationHelper.CreatePagedReponse(postWithPaging, validFilter, totalRecords, _uriService, route);

            return Ok(pagedReponse);
        }

        [HttpGet("general")]
        public async Task<IActionResult> GetGeneral([FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);

            var postList = await _postService.GetGeneralPost();
            var postWithPaging = postList
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToList();

            var totalRecords = postList.Count();
            var pagedReponse =
                PaginationHelper.CreatePagedReponse(postWithPaging, validFilter, totalRecords, _uriService, route);

            return Ok(pagedReponse);
        }

        [HttpGet("{idPost:int}/comment")]
        public async Task<IActionResult> GetAllCommentsByPostId(int idPost)
        {
            var postCommentList = await _postService.GetAllCommentByPostId(idPost);
            return Ok(new Response<List<CommentWrapper>>(postCommentList));
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(NewPostDTO newPost)
        {
            var newPostSaved = await _postService.SaveNewPost(newPost);
            return StatusCode(201, newPostSaved);
        }

        [HttpPatch]
        public async Task<IActionResult> UpdatePost(EditPostDTO newPostDTO)
        {
            await _postService.UpdatePost(newPostDTO);
            return StatusCode(200, newPostDTO);
        }

        [HttpDelete("{idPost:int}")]
        public async Task<IActionResult> DeletePost(int idPost)
        {
            await _postService.DeletePost(idPost);
            return StatusCode(200);
        }

        [HttpPost("comment")]
        public async Task<IActionResult> CreateComment(Comment comment)
        {
            await _postService.SaveNewComment(comment);
            return StatusCode(201, comment);
        }

        [HttpDelete("comment/{idComment:int}")]
        public async Task<IActionResult> DeleteComment(int idComment)
        {
            await _postService.DeleteComment(idComment);
            return StatusCode(200);
        }


        [HttpPatch("comment")]
        public async Task<IActionResult> UpdateComment(Comment newComment)
        {
            await _postService.UpdateComment(newComment);
            return StatusCode(200);
        }
    }
}