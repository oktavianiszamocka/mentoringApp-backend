using MentorApp.Models;
using MentorApp.Repository;
using MentorApp.Wrappers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentorApp.Services
{
    public interface IPostService
    {
        Task<List<PostWrapper>> GetAll();
        Task<List<PostWrapper>> GetPostProject(int IdProject);
        Task<List<PostWrapper>> GetGeneralPost();
        Task<List<CommentWrapper>> GetAllCommentByPostId(int IdPost);

        Task<Post> SaveNewPost(Post post);

    }
}
