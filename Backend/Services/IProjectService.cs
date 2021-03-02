using MentorApp.DTOs.Requests;
using MentorApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MentorApp.Services
{
    public interface IProjectService
    {
        Task<ProjectInfoDTO> GetProjectInfoById(int idProject);
        Task<Project> SaveNewProject(NewProjectDTO project);
        Task<List<ProjectStatus>> GetAllProjectStatus();
    }
}
