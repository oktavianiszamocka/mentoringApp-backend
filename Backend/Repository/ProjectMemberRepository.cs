using MentorApp.Models;
using MentorApp.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentorApp.Repository
{
    public class ProjectMemberRepository : IProjectMemberRepository
    {
        private readonly MentorAppContext _context;

        public ProjectMemberRepository(MentorAppContext context)
        {
            _context = context;
        }

        public async Task<List<Project>> GetProjectByNameSearch(int IdUser, String SearchString)
        {
            return await _context.ProjectMembers
                        .Include(project => project.ProjectNavigation)
                        .ThenInclude(superVisor => superVisor.SuperviserNavigation)
                        .Where(project => project.Member.Equals(IdUser) && project.ProjectNavigation.Name.StartsWith(SearchString))
                        .Select(project => project.ProjectNavigation)
                        .ToListAsync();
        }


        public async Task<List<Project>> GetProjectName(int IdUser)
        {
            return await _context.ProjectMembers
                        .Include(project => project.ProjectNavigation)
                        .ThenInclude(superVisor => superVisor.SuperviserNavigation)
                        .Where(project => project.Member.Equals(IdUser))
                        .Select(project => project.ProjectNavigation)
                        .ToListAsync();
        }

        public async Task<List<ProjectMembers>> GetProjectMembers(int IdProject)
        {
            return await _context.ProjectMembers
                        .Include(member => member.MemberRoleNavigation)
                        .Include(project => project.MemberNavigation)
                        .ThenInclude(user => user.Profile)
                        .Where(project => project.Project.Equals(IdProject))
                        .ToListAsync();

        }

    }
}
