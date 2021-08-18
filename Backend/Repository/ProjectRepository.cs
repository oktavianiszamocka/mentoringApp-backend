using MentorApp.Models;
using MentorApp.Persistence;
using Microsoft.EntityFrameworkCore;
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

        public async Task<List<ProjectStatus>> GetAllProjectStatus()
        {
            return await _context.ProjectStatus
                        .ToListAsync();
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

        public async Task<Project> SaveNewProject(Project project)
        {
            var newProject = await _context.Project.AddAsync(project);
            await _context.SaveChangesAsync();
            return newProject.Entity;
        }

        public async Task<Project> UpdateProject(Project projectToUpdate)
        {
            var projectToUpdateDb = await _context.Project.FindAsync(projectToUpdate.IdProject);
            projectToUpdateDb.Name = projectToUpdate.Name;
            projectToUpdateDb.Description = projectToUpdate.Description;
            projectToUpdateDb.StartDate = projectToUpdate.StartDate;
            projectToUpdateDb.EndDate = projectToUpdate.EndDate;
            projectToUpdateDb.Status = projectToUpdate.Status;
            _context.Project.Update(projectToUpdateDb);
            await _context.SaveChangesAsync();
            return projectToUpdateDb;

        }
    }
}
