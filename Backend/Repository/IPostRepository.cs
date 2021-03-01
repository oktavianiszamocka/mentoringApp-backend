
using System.Collections.Generic;

using MentorApp.Wrappers;
using System.Threading.Tasks;
using MentorApp.Models;

namespace MentorApp.Repository
{
    public interface IPostRepository
    {
        Task<List<Post>> GetAllPost();
        Task<List<Post>> GetPostByProject(int IdProject);
        Task<List<Post>> GetGeneralPost();
        Task<Post> GetAllCommentByPostId(int IdPost);
        Task<Post> GetPostById(int IdPost);
        Task<List<PostTag>> GetAllPostTagByPostId (int IdPost);
        Task<Post> SaveNewPost(Post post);
        Task<Tag> SaveNewTag(Tag newTag);
        Task<Tag> GetTagByName(string tag);
        Task<PostTag> SaveNewPostTag(PostTag postTag);
        Task<Comment> SaveNewComment(Comment comment);
        Task<Post> DeletePost(int PostId);
        Task<Comment> DeleteComment(int IdComment);
        Task<PostTag> DeletePostTag(int PostTagId);
        Task<Post> UpdatePost(Post post);
        Task<Comment> UpdateComment(Comment comment);

    }
}
