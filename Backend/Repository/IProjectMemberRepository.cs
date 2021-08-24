using MentorApp.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

namespace MentorApp.Repository
{
    public interface IProjectMemberRepository
    {
        Task<List<Project>> GetProjectName(int IdUser);
        Task<List<Project>> GetProjectByNameSearch(int IdUser, String SearchString);
        Task<List<ProjectMembers>> GetProjectMembers(int IdProject);
        Task<User> GetStudentByEmail(string emailUser);

        Task<ProjectMembers> CreateNewProjectMember(ProjectMembers newMember);

        Task<List<MemberRole>> GetMemberRoles();

        Task<Boolean> IsProjectLeaderExistInProject(int idProject);

        Task<ProjectMembers> RemoveProjectMember(int idProjectMember);

        Task<ProjectMembers> UpdateProjectRole(int idProjectMember, int newRole);
    }
}
