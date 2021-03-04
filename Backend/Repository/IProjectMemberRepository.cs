using MentorApp.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MentorApp.Repository
{
    public interface IProjectMemberRepository
    {
        Task<List<Project>> GetProjectName(int IdUser);
        Task<List<Project>> GetProjectByNameSearch(int IdUser, String SearchString);
        Task<List<ProjectMembers>> GetProjectMembers(int IdProject);
    }
}
