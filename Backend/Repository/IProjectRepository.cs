using System;
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
        Task<List<ProjectStudies>> GetAllProjectStudies();
        Task<List<UrlType>> GetAllUrlType();
        Task<List<Models.Url>> GetAllProjectUrls(int idProject);
        Task<List<ProjectMode>> GetAllProjectModes();

        Task<List<Project>> GetProjectBySuperviser(int idUser);
        Task<Project> UpdateProject(Project projectToUpdate);
        Task<Project> UpdateIcon(int idProject, String iconUrl);

        Task<Url> SaveNewProjectUrl(Url newUrl);
        Task<List<Url>> DeleteOldUrl(int idProject);
    }
}
