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
        private readonly MentorAppContext _context;
        private readonly IUriService _uriService;

        public PostsController(MentorAppContext context, IUriService uriService)
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

            var postList = await _context.Post
                           .Include(post => post.WriterNavigation)
                           .Include(post => post.PostTag)
                           .ThenInclude(postTag => postTag.TagNavigation)
                           .Include(post => post.Comment)
                           .ThenInclude(comment => comment.CreatedByNavigation)
                          .Select(post => new PostWrapper
                          {
                              IdPost = post.IdPost,
                              Title = post.Title,
                              Content = post.Content,
                              DateOfPublication = post.DateOfPublication,
                              Writer = new UserWrapper
                              {
                                  firstName = post.WriterNavigation.FirstName,
                                  lastName = post.WriterNavigation.LastName,
                                  imageUrl = post.WriterNavigation.Avatar

                              },
                              NewestComment = post.Comment.OrderByDescending(comment => comment.CreatedOn).Select(comment => new CommentWrapper
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
                              }).FirstOrDefault(),
                              //post.Comment.OrderByDescending(comment => comment.CreatedOn).ToList(),
                              tags = post.PostTag.Select(postTag => new string
                              ( postTag.TagNavigation.Name

                              )).ToList()
                           

                          }) 
                         .OrderByDescending(post => post.DateOfPublication)
                         .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                         .Take(validFilter.PageSize)
                         .ToListAsync();

            var totalRecords = await _context.Post.CountAsync();
            var pagedReponse = PaginationHelper.CreatePagedReponse<PostWrapper>(postList, validFilter, totalRecords, _uriService, route);
            
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