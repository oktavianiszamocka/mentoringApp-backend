﻿
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
    }
}
