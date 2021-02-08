using MentorApp.DTOs.Requests;
using MentorApp.Models;
using MentorApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentorApp.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<ProjectInfoDTO> GetProjectInfoById(int idProject)
        {
            var projectInfo =  await _projectRepository.GetProjectInfoById(idProject);
            var projectInfoDTO = new ProjectInfoDTO
            {
                Name = projectInfo.Name,
                Description = projectInfo.Description,
                StartDate = projectInfo.StartDate,
                EndDate = projectInfo.EndDate,
                StatusName = projectInfo.StatusNavigation.Name,
                SuperviserFirstName = projectInfo.SuperviserNavigation.FirstName,
                SuperviserLastName = projectInfo.SuperviserNavigation.LastName,
                Icon = projectInfo.Icon
            };

            return projectInfoDTO;
        }
    }
}
