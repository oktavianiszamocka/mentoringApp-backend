using MentorApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentorApp.Repository
{
    interface IProjectsListRepository
    {
        Task<List<Project>> GetProjectsById(int idUser);
    }
}
