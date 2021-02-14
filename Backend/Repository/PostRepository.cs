using MentorApp.Models;
using MentorApp.Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;

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
                            .ThenByDescending(post => post.IdPost)
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

        public async Task<Tag> SaveNewTag(Tag newTag)
        {
            _context.Tag.Add(newTag);
            await _context.SaveChangesAsync();
            return newTag;
        }

        public async Task<PostTag> SaveNewPostTag(PostTag postTag)
        {
            _context.PostTag.Add(postTag);
            await _context.SaveChangesAsync();
           
            return postTag;
        }

        public async Task<Comment> SaveNewComment(Comment comment)
        {
            _context.Comment.Add(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<Tag> GetTagByName(string tagName)
        {
            return await _context.Tag
                            .Where(tag => tag.Name.Equals(tagName) || tag.Name.Equals(tagName.ToLower()))
                            .FirstOrDefaultAsync();
        }

        public async Task<Post> DeletePost(int PostId)
        {
            var post = await _context.Post.FindAsync(PostId);
            _context.Post.Remove(post);
            await _context.SaveChangesAsync();
            return post;
        }

        public async Task<PostTag> DeletePostTag(int PostTagId)
        {
            var postTag = await _context.PostTag.FindAsync(PostTagId);
            _context.PostTag.Remove(postTag);
            await _context.SaveChangesAsync();
            return postTag;
        }

        public async Task<Post> GetPostById(int IdPost)
        {
            var post = await _context.Post.FindAsync(IdPost);
            return post;
        }

        public async Task<List<PostTag>> GetAllPostTagByPostId(int IdPost)
        {
            return await _context.PostTag
                           .Include(postTag => postTag.TagNavigation)
                           .Where(postTag => postTag.Post.Equals(IdPost))
                           .ToListAsync();
        }

        public async Task<Post> UpdatePost(Post post)
        {
            var existingPost = await _context.Post.FindAsync(post.IdPost);
            existingPost.Title = post.Title;
            existingPost.Content = post.Content;
            _context.Post.Update(existingPost);
            await _context.SaveChangesAsync();
            return post;
        }
    }
}
