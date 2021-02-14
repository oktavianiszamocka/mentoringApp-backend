using MentorApp.Models;
using MentorApp.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentorApp.Repository
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly MentorAppContext _context;

        public ProjectRepository(MentorAppContext context)
        {
            _context = context;
        }

        public async Task<Project> GetProjectInfoById(int idProject)
        {
            return await _context.Project
                         .Include(project => project.StatusNavigation)
                         .Include(project => project.SuperviserNavigation)
                         .Include(project => project.ProjectMembers)
                         .Include(project => project.Url)
                         .Include(project => project.ProjectMembers)
                         .ThenInclude(projectMember => projectMember.MemberNavigation)
                         .Where(project => project.IdProject.Equals(idProject))
                         .FirstOrDefaultAsync();
        }

    }
}
