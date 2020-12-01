
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
        Task<Post> SaveNewPost(Post post);
        Task<Tag> SaveNewTag(Tag newTag);
        Task<PostTag> SaveNewPostTag(PostTag postTag);

    }
}
