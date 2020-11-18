using MentorApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentorApp.Services
{
    public class ProjectMemberService : IProjectMemberService
    {
        private readonly IProjectMemberRepository _projectMemberRepository;
        public ProjectMemberService(IProjectMemberRepository projectMemberRepository)
        {
            _projectMemberRepository = projectMemberRepository;
        }
        public async Task<List<string>> GetProjectsNameByIdUser(int IdUser)
        {
            return await _projectMemberRepository.GetProjectName(IdUser);
        }
    }
}
