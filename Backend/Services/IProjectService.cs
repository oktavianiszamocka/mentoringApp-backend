using System;
using MentorApp.DTOs.Requests;
using MentorApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using MentorApp.DTOs.Responses;

namespace MentorApp.Services
{
    public interface IProjectService
    {
        Task<ProjectInfoDTO> GetProjectInfoById(int idProject);
        Task<Project> SaveNewProject(NewProjectDTO project);
        Task<List<DropdownDTO>> GetAllProjectStatus();
        Task<List<DropdownDTO>> GetAllProjectStudies();
        Task<List<DropdownDTO>> GetAllProjectMode();
        Task<List<DropdownDTO>> GetAllUrlType();
        Task<List<Models.Url>> GetAllProjectUrls(int idProject);
        Task<List<Models.Url>> SaveNewProjectUrl(List<Models.Url> newUrls);
        Task<Project> UpdateProject(Project project);
        Task<Project> UpdateIcon(int idProject, String iconUrl);
        Task<Project> DeleteProject(int idProject);
    }
}
