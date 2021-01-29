using MentorApp.DTOs.Responses;
using MentorApp.Models;
using MentorApp.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentorApp.Services
{
    public interface IProjectMemberService
    {
        Task<List<ProjectDTO>> GetProjectsNameByIdUser(int IdUser);
        Task<List<ProjectWrapper>> GetProjectsByIdUser(int IdUser);
        Task<List<ProjectWrapper>> GetProjectByNameSearch (int IdUser, String SearchString);

        Task<List<ProjectMemberDTO>> GetProjectMembers(int IdProject);
    }
}
