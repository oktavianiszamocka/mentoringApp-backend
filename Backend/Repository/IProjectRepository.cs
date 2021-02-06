using MentorApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentorApp.Repository
{
    public interface IProjectRepository
    {
        Task<Project> GetProjectById(int idProject);
    }
}
