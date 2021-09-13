using MentorApp.DTOs.Responses;
using MentorApp.Wrappers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MentorApp.DTOs.Requests;
using MentorApp.Models;
using Microsoft.CodeAnalysis;
using Task = MentorApp.Models.Task;

namespace MentorApp.Services
{
    public interface IProjectMemberService
    {
        Task<List<ProjectDTO>> GetProjectsNameByIdUser(int IdUser);
        Task<List<ProjectWrapper>> GetProjectsByIdUser(int IdUser);
        Task<List<ProjectWrapper>> GetProjectByNameSearch(int IdUser, String SearchString);

        Task<List<ProjectMemberDTO>> GetProjectMembers(int IdProject);
        Task<NewProjectMembersDTO> CreateProjectMembers(NewProjectMembersDTO newProjectMembersDTO);
        Task<ProjectMembers> InsertProjectMember(Invitation invitation);

        Task<List<DropdownDTO>> GetMemberRoles();
        Task<ProjectMembers> DeleteProjectMember(int idProjectMember);

        Task<ProjectMembers> UpdateProjectMember(ProjectMemberUpdateWrapper editProjectMember);
    }
}
