using MentorApp.DTOs.Requests;
using MentorApp.Helpers;
using MentorApp.Models;
using MentorApp.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MentorApp.DTOs.Responses;

namespace MentorApp.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectPromotersRepository _projectPromotersRepository;
        private readonly IMapper _mapper;
        public ProjectService(IProjectRepository projectRepository, IProjectPromotersRepository projectPromotersRepository,IMapper mapper )
        {
            _projectRepository = projectRepository;
            _projectPromotersRepository = projectPromotersRepository;
            _mapper = mapper;
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

        public async Task<List<DropdownDTO>> GetAllProjectStudies()
        {
            var projectStudies = await _projectRepository.GetAllProjectStudies();
            var projectStudiesDTO = projectStudies.Select(studies => new DropdownDTO
            {
                Value = studies.IdProjectStudies,
                Label = studies.Name
            }).ToList();
            return projectStudiesDTO;
        }

        public async Task<List<DropdownDTO>> GetAllProjectMode()
        {
            var projectMode = await _projectRepository.GetAllProjectModes();
            var projectModesDTO = projectMode.Select(mode => new DropdownDTO
            {
                Value = mode.IdProjectMode,
                Label = mode.Name
            }).ToList();
            return projectModesDTO;
        }

        public async Task<List<DropdownDTO>> GetAllUrlType()
        {
            var urlTypes = await _projectRepository.GetAllUrlType();
            var urlTypeDropdown = urlTypes.Select(type => new DropdownDTO
            {
                Value = type.IdUrlType,
                Label = type.UrlName
            }).ToList();
            return urlTypeDropdown;
        }

        public async Task<List<Url>> GetAllProjectUrls(int idProject)
        {
            return await _projectRepository.GetAllProjectUrls(idProject);
        }

        public async Task<List<Url>> SaveNewProjectUrl(List<Url> newUrls)
        {
            var idProject = newUrls[0].Project;
            List<Url> oldUrls = await this.GetAllProjectUrls(idProject);

            if (oldUrls.Count > 0)
            {
                await _projectRepository.DeleteOldUrl(idProject);
            }


            List<Url> newInsertedUrls = new List<Url>();
            
            foreach (var newUrl in newUrls)
            {
                newInsertedUrls.Add( await _projectRepository.SaveNewProjectUrl(newUrl));
            }

            return newInsertedUrls;
        }

        public async Task<Project> UpdateProject(Project project)
        {
            return await _projectRepository.UpdateProject(project);
        }

        public async Task<Project> UpdateIcon(int idProject, string iconUrl)
        {
            return await _projectRepository.UpdateIcon(idProject, iconUrl);
        }

        public async Task<Project> DeleteProject(int idProject)
        {
            return await _projectRepository.DeleteProject(idProject);
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

            List<UrlDTO> urlDtos = new List<UrlDTO>();

            if (projectInfo.Url.Count > 0)
            {
                urlDtos = _mapper.Map<List<UrlDTO>>(projectInfo.Url);

            }
          


            var projectInfoDTO = new ProjectInfoDTO
            {
                IdProject =  projectInfo.IdProject,
                Name = projectInfo.Name,
                Description = projectInfo.Description,
                StartDate = projectInfo.StartDate,
                EndDate = projectInfo.EndDate,
                Status = projectInfo.Status,
                StatusName =  projectInfo.StatusNavigation.Name ,
                Studies = projectInfo.Studies,
                StudiesName = projectInfo.Studies != null ? projectInfo.StudiesNavigation.Name : "-",
                Mode = projectInfo.Mode,
                ModeName = projectInfo.Mode != null ?  projectInfo.ModeNavigation.Name : "-",
                Superviser = projectInfo.Superviser,
                SuperviserEmail =  projectInfo.SuperviserNavigation.Email,
                SuperviserFirstName = projectInfo.SuperviserNavigation.FirstName,
                SuperviserLastName = projectInfo.SuperviserNavigation.LastName,
                Icon = projectInfo.Icon != null ? projectInfo.Icon : null ,
                projectLeaderFirstName = leaderFirstName,
                projectLeaderLastName = leaderLastName,
                UrlLinks = urlDtos.Count > 0 ? urlDtos : null
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
            if (project.Studies != 0)
            {
                newproject.Studies = project.Studies;
            }

            if (project.Mode != 0)
            {
                newproject.Mode = project.Mode;
            }

            var newProjectInserted = await _projectRepository.SaveNewProject(newproject);
            return newProjectInserted;
        }
    }
}
