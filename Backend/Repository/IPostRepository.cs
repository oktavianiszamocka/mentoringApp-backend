
using MentorApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MentorApp.Repository
{
    public interface IPostRepository
    {
        Task<List<Post>> GetAllPost();
        Task<List<Post>> GetPostByProject(int IdProject);
        Task<List<Post>> GetGeneralPost();
        Task<Post> GetAllCommentByPostId(int IdPost);
        Task<Post> GetPostById(int IdPost);
        Task<List<PostTag>> GetAllPostTagByPostId(int IdPost);
        Task<Post> SaveNewPost(Post post);
        Task<Tag> SaveNewTag(Tag newTag);
        Task<Tag> GetTagByName(string tag);
        Task<PostTag> SaveNewPostTag(PostTag postTag);
        Task<Comment> SaveNewComment(Comment comment);
        Task<Post> DeletePost(int PostId);
        Task<PostTag> DeletePostTag(int PostTagId);
        Task<Post> UpdatePost(Post post);

    }
}
