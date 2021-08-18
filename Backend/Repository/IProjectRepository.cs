using MentorApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MentorApp.Repository
{
    public interface IProjectRepository
    {
        Task<Project> GetProjectInfoById(int idProject);
        Task<Project> SaveNewProject(Project project);
        Task<List<ProjectStatus>> GetAllProjectStatus();

        Task<Project> UpdateProject(Project projectToUpdate);
    }
}
