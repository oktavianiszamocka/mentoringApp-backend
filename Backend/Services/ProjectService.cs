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

            var leaderFirstName = "";
            var leaderLastName = "";
            foreach (ProjectMembers member in projectInfo.ProjectMembers)
            {
                if (member.Role.Equals(1))
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
                Name = projectInfo.Name,
                Description = projectInfo.Description,
                StartDate = projectInfo.StartDate,
                EndDate = projectInfo.EndDate,
                StatusName = projectInfo.StatusNavigation.Name,
                SuperviserFirstName = projectInfo.SuperviserNavigation.FirstName,
                SuperviserLastName = projectInfo.SuperviserNavigation.LastName,
                Icon = projectInfo.Icon,
                projectLeaderFirstName = leaderFirstName,
                projectLeaderLastName = leaderLastName,
                UrlLinks = links
            };

            return projectInfoDTO;
        }
    }
}
