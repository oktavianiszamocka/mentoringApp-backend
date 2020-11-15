using MentorApp.Models;
using MentorApp.Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MentorApp.Wrappers;
using Microsoft.EntityFrameworkCore;


namespace MentorApp.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly MentorAppContext _context;

        public PostRepository(MentorAppContext context)
        {
            _context = context;
        }

        public async Task<List<Post>> GetAllPost()
        {
            return  await _context.Post
                           .Include(post => post.WriterNavigation)
                           .Include(post => post.PostTag)
                           .ThenInclude(postTag => postTag.TagNavigation)
                           .Include(post => post.Comment)
                           .ThenInclude(comment => comment.CreatedByNavigation)
                           .OrderByDescending(post => post.DateOfPublication)
                         .ToListAsync();
        }

        public async Task<List<Post>> GetPostByProject(int IdProject)
        {
            return await _context.Post
                            .Where(post => post.Project.Equals(IdProject))
                           .Include(post => post.WriterNavigation)
                           .Include(post => post.PostTag)
                           .ThenInclude(postTag => postTag.TagNavigation)
                           .Include(post => post.Comment)
                           .ThenInclude(comment => comment.CreatedByNavigation)
                           .OrderByDescending(post => post.DateOfPublication)
                         .ToListAsync();
        }
    }
}
