using MentorApp.DTOs.Requests;
using MentorApp.Helpers;
using MentorApp.Models;
using MentorApp.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MentorApp.DTOs.Responses;

namespace MentorApp.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectPromotersRepository _projectPromotersRepository;
        public ProjectService(IProjectRepository projectRepository, IProjectPromotersRepository projectPromotersRepository)
        {
            _projectRepository = projectRepository;
            _projectPromotersRepository = projectPromotersRepository;
        }

        public async Task<List<DropdownDTO>> GetAllProjectStatus()
        {
            var projectStatus = await _projectRepository.GetAllProjectStatus();
            var projectStatusDTO = projectStatus.Select(status => new DropdownDTO
            {
                Value = status.IdStatus,
                Label = status.Name
            }).ToList();
            return projectStatusDTO;
        }

        public async Task<Project> UpdateProject(Project project)
        {
            return await _projectRepository.UpdateProject(project);
        }

        public async Task<ProjectInfoDTO> GetProjectInfoById(int idProject)
        {
            var projectInfo = await _projectRepository.GetProjectInfoById(idProject);

            var leaderFirstName = "";
            var leaderLastName = "";
            foreach (ProjectMembers member in projectInfo.ProjectMembers)
            {
                if(member.Role.Equals(1))
                {
                    leaderFirstName = member.MemberNavigation.FirstName;
                    leaderLastName = member.MemberNavigation.LastName;
                }
            }

            List<string> links = new List<string>();
            foreach (Url u in projectInfo.Url)
            {
                links.Add(u.Link);
            }

            var projectInfoDTO = new ProjectInfoDTO
            {
                IdProject =  projectInfo.IdProject,
                Name = projectInfo.Name,
                Description = projectInfo.Description,
                StartDate = projectInfo.StartDate,
                EndDate = projectInfo.EndDate,
                Status = projectInfo.Status,
                StatusName = projectInfo.StatusNavigation.Name,
                Superviser = projectInfo.Superviser,
                SuperviserEmail =  projectInfo.SuperviserNavigation.Email,
                SuperviserFirstName = projectInfo.SuperviserNavigation.FirstName,
                SuperviserLastName = projectInfo.SuperviserNavigation.LastName,
                Icon = projectInfo.Icon,
                projectLeaderFirstName = leaderFirstName,
                projectLeaderLastName = leaderLastName,
                UrlLinks = links
            };

            return projectInfoDTO;
        }

        public async Task<Project> SaveNewProject(NewProjectDTO project)
        {
            var promoter = await _projectPromotersRepository.GetProjectPromoterByEmail(project.SuperviserEmail);
            if (promoter == null)
            {
                throw new HttpResponseException("Superviser Email is not found. Please enter correct supervisor email");
            }

            var newproject = new Project
            {
                Name = project.Name,
                Description = project.Description,
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                Status = project.Status,
                Superviser = promoter.IdUser,
            };

            var newProjectInserted = await _projectRepository.SaveNewProject(newproject);
            return newProjectInserted;
        }
    }
}
