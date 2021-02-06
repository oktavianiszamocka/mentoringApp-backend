using MentorApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentorApp.Services
{
    public interface IProjectService
    {
        Task<Project> GetProjectById(int idProject);
    }
}
