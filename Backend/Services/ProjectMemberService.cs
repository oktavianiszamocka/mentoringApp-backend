using AutoMapper;
using MentorApp.DTOs.Responses;
using MentorApp.Models;
using MentorApp.Repository;
using MentorApp.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MentorApp.DTOs.Requests;
using MentorApp.Helpers;

namespace MentorApp.Services
{

    public class ProjectMemberService : IProjectMemberService
    {
        private readonly IProjectMemberRepository _projectMemberRepository;
        private readonly IInvitationRepository _invitationRepository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IMailService _mailService;
        private readonly IProjectRepository _projectRepository;
        
        public ProjectMemberService(IProjectMemberRepository projectMemberRepository, IMapper mapper, IInvitationRepository invitationRepository, IMailService mailService, IUserRepository userRepository, IProjectRepository projectRepository)
        {
            _projectMemberRepository = projectMemberRepository;
            _mapper = mapper;
            _invitationRepository = invitationRepository;
            _mailService = mailService;
            _userRepository = userRepository;
            _projectRepository = projectRepository;
        }

        public async Task<List<ProjectDTO>> GetProjectsNameByIdUser(int IdUser)
        {
            var projectList = await _projectMemberRepository.GetProjectName(IdUser);
            var projectDTOList = _mapper.Map<List<ProjectDTO>>(projectList);
            return projectDTOList;
        }

        public async Task<List<ProjectWrapper>> GetMyProjectFiltered(int IdUser, String SearchString, int? study, int? mode)
        {
            Boolean isUserMentor = await _userService.IsUserMentor(IdUser);
            List<Project> projectList = new List<Project>();

            if (isUserMentor)
            {
                projectList = await _projectPromotersRepository.GetPromotorProjects(IdUser);
                projectList.AddRange(await _projectRepository.GetProjectBySuperviser(IdUser));
                
            }
            else
            {
                projectList = await _projectMemberRepository.GetMyProjectFiltered(IdUser);
            }

            if (SearchString == null)
            {
                SearchString = "";
            }

            if (projectList.Count > 0)
            {
                List<Project> filtredProjects = projectList;
                if (study != null && study != 0)
                {
                    filtredProjects = filtredProjects.Where(project => project.Studies.Equals(study)).ToList();

                }

                if (mode != null && mode != 0)
                {
                    filtredProjects = filtredProjects.Where(project => project.Mode.Equals(mode)).ToList();

                }

                if (!String.IsNullOrEmpty(SearchString))
                {
                    filtredProjects = filtredProjects.Where(project => project.Name.Contains(SearchString)).ToList();
                }

                var projectDTOList = GetProjectWrappers(filtredProjects);
                return projectDTOList;

            }
            else
            {
                throw new HttpResponseException("Sorry! No project found");
            }

        }
        public async Task<List<ProjectWrapper>> GetProjectsByIdUser(int IdUser)
        {
            var projectList = await _projectMemberRepository.GetProjectName(IdUser);
            var projectDTOList = GetProjectWrappers(projectList);
            return projectDTOList; 

        }

        public List<ProjectWrapper> GetProjectWrappers(List<Project> projects)
        {
            var projectDTOList = projects
                      .Select(project => new ProjectWrapper
                      {
                          IdProject = project.IdProject,
                          Name = project.Name,
                          Description = project.Description,
                          StartDate = project.StartDate,
                          EndDate = project.EndDate,
                          Superviser = project.Superviser,
                          SuperviserFullName = project.SuperviserNavigation.FirstName + " " + project.SuperviserNavigation.LastName,
                          StatusName = project.StatusNavigation.Name,
                          StudyName = project.Studies != null ? project.StudiesNavigation.Name : null,
                          ModeName = project.Mode != null ? project.ModeNavigation.Name : null

                      }).ToList();

            return projectDTOList;
        }

        public async Task<List<ProjectMemberDTO>> GetProjectMembers(int IdProject)
        {
            var projectMembers = await _projectMemberRepository.GetProjectMembers(IdProject);

            var projectMembersDTO = projectMembers
                                .Select(member => new ProjectMemberDTO
                                {
                                    IdUser = member.Member,
                                    FirstName = member.MemberNavigation.FirstName,
                                    LastName = member.MemberNavigation.LastName,
                                    Email = member.MemberNavigation.Email,
                                    IdProjectMember = member.IdProjectMember,
                                    Avatar = member.MemberNavigation.Avatar,
                                    Role = member.MemberRoleNavigation.IdRoleMember,
                                    ProjectRole = member.MemberRoleNavigation.Role,
                                    Major = member.MemberNavigation.Profile.FirstOrDefault().Major,
                                    Semester = member.MemberNavigation.Profile.FirstOrDefault().Semester
                                }).ToList();

            return projectMembersDTO;
        }


        public async Task<NewProjectMembersDTO> CreateProjectMembers(NewProjectMembersDTO newProjectMembersDTO)
        {
            List<Invitation> invitationsToInsert = new List<Invitation>();
            Boolean hasProjectLeader = false;
          
        
            for (int i = 0; i < newProjectMembersDTO.NewMembers.Count; i++)
            {
                await searchStudentAndCreateInvitation(newProjectMembersDTO.NewMembers[i].MemberEmail, newProjectMembersDTO.NewMembers[i].Role, i + 1,
                    newProjectMembersDTO.IdProject, invitationsToInsert);
                if (newProjectMembersDTO.NewMembers[i].Role.Equals(1))
                {
                    hasProjectLeader = true;
                }
            }

            var projectLeaderExist =
                await _projectMemberRepository.IsProjectLeaderExistInProject(newProjectMembersDTO.IdProject);
            var projectLeaderInvitation =
               await _invitationRepository.IsProjectMemberLeaderInvitationExist(newProjectMembersDTO.IdProject);

            if (projectLeaderExist || projectLeaderInvitation)
            {
                hasProjectLeader = true;

            }

            if (!hasProjectLeader && invitationsToInsert.Count == 1)
            {
                invitationsToInsert[0].Role = 1;
                hasProjectLeader = true;
            }

            if(!hasProjectLeader)
            {
                throw new HttpResponseException("Please choose one of project member to be Project Leader");
            }

            if (invitationsToInsert.Count > 0 && hasProjectLeader)
            {
                await _invitationRepository.CreateManyInvitations(invitationsToInsert);

                foreach (var member in newProjectMembersDTO.NewMembers)
                {
                    var user = await _userRepository.GetUserByEmail(member.MemberEmail);
                    var project = await _projectRepository.GetProjectInfoById(newProjectMembersDTO.IdProject);
                    await _mailService.InviteToProject(user.FirstName, user.Email, project.Name);
                }
            }


            return newProjectMembersDTO;
        }

        public async Task<ProjectMembers> InsertProjectMember(Invitation invitation)
        {
            var newMember = new ProjectMembers
            {
                Project = invitation.Project,
                Member = invitation.For_Who,
                Role = (int) invitation.Role
            };

            var inserted = await _projectMemberRepository.CreateNewProjectMember(newMember);
            return inserted;

        }

        public async Task<List<DropdownDTO>> GetMemberRoles()
        {
            var roles = await _projectMemberRepository.GetMemberRoles();
            var rolesDto = roles.Select(role => new DropdownDTO
            {
                Label = role.Role,
                Value = role.IdRoleMember
            }).ToList();
            return rolesDto;
        }

        public async Task<ProjectMembers> DeleteProjectMember(int idProjectMember)
        {
            var projectMemberDeleted = await _projectMemberRepository.RemoveProjectMember(idProjectMember);
            return projectMemberDeleted;
        }

        public async Task<ProjectMembers> UpdateProjectMember(ProjectMemberUpdateWrapper editProjectMember)
        {
            
            var projectMember =      await _projectMemberRepository.UpdateProjectRole(editProjectMember.IdProjectMember,
                        editProjectMember.IdNewRole);
            
            return projectMember;
        }

        public async Task<Invitation> searchStudentAndCreateInvitation(String userEmail, int role, int index, int idProject, List<Invitation> invitations)
        {
            var newInvitation = new Invitation();

            var student = await _projectMemberRepository.GetStudentByEmail(userEmail);
            if (student == null)
            {
                throw new HttpResponseException(
                                                userEmail + " is not found. Please enter correct student email");
            }
            else
            {
                var invitation = new Invitation
                {
                    Project = idProject,
                    For_Who = student.IdUser,
                    Role = role,
                    IsMemberInvitation = true
                };
                invitations.Add(invitation);
            }

            return newInvitation;
        }
    }
}
