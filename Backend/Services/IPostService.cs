using MentorApp.DTOs.Requests;
using MentorApp.Models;
using MentorApp.Wrappers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MentorApp.Services
{
    public interface IPostService
    {
        Task<List<PostWrapper>> GetAll();
        Task<List<PostWrapper>> GetPostProject(int IdProject);
        Task<List<PostWrapper>> GetGeneralPost();
        Task<List<CommentWrapper>> GetAllCommentByPostId(int IdPost);

        Task<Post> SaveNewPost(NewPostDTO post);
        Task<Post> UpdatePost(EditPostDTO post);
        Task<Comment> SaveNewComment(Comment comment);
        Task<Post> DeletePost(int IdPost);
        Task<Comment> DeleteComment(int IdComment);
        Task<Comment> UpdateComment(Comment comment);

    }
}
