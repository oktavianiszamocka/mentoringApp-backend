using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PagedList;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly s17874Context _context;

        public PostController(s17874Context context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetPostList(int pageNumber, int pageSize)
        {
            var result = _context.Post.OrderByDescending(post => post.DateOfPublication).ToList();
            return Ok(result.ToPagedList(pageNumber, pageSize));
        }
    }
}