using MentorApp.Models;
using MentorApp.Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<List<Post>> GetGeneralPost()
        {
            return await _context.Post
                             .Where(post => !post.Project.HasValue)
                            .Include(post => post.WriterNavigation)
                            .Include(post => post.PostTag)
                            .ThenInclude(postTag => postTag.TagNavigation)
                            .Include(post => post.Comment)
                            .ThenInclude(comment => comment.CreatedByNavigation)
                            .OrderByDescending(post => post.DateOfPublication)
                          .ToListAsync();
        }
        public async Task<Post> GetAllCommentByPostId(int IdPost)
        {
            return await _context.Post
                                .Where(post => post.IdPost.Equals(IdPost))
                                .Include(post => post.Comment)
                                .ThenInclude(comment => comment.CreatedByNavigation)
                                .FirstOrDefaultAsync();
            
        }

        public async Task<Post> SaveNewPost(Post post)
        {
            _context.Post.Add(post);
            await _context.SaveChangesAsync();
            return post;
        }

        public Task<Tag> SaveNewTag(Tag newTag)
        {
            throw new System.NotImplementedException();
        }

        public Task<PostTag> SaveNewPostTag(PostTag postTag)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Comment> SaveNewComment(Comment comment)
        {
            _context.Comment.Add(comment);
            await _context.SaveChangesAsync();
            return comment;
        }
    }
}
