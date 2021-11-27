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

        public async Task<List<ProjectStudies>> GetAllProjectStudies()
        {
            return await _context.ProjectStudies.ToListAsync();
        }

        public async Task<List<UrlType>> GetAllUrlType()
        {
            return await _context.UrlType.ToListAsync();
        }

        public async Task<List<Url>> GetAllProjectUrls(int idProject)
        {
            return await _context.Url.Where(pro => pro.Project.Equals(idProject)).ToListAsync();
        }

        public async Task<Url> SaveNewProjectUrl(Url newUrl)
        {
            var newProjectUrl = await _context.Url.AddAsync(newUrl);
            await _context.SaveChangesAsync();
            return newProjectUrl.Entity;
        }

       

        public async Task<List<ProjectMode>> GetAllProjectModes()
        {
            return await _context.ProjectModes.ToListAsync();
        }

        public async Task<Project> GetProjectInfoById(int idProject)
        {
            return await _context.Project
                         .Include(project => project.StatusNavigation)
                         .Include(project => project.StudiesNavigation)
                         .Include(project => project.ModeNavigation)
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
            projectToUpdateDb.Studies = projectToUpdate.Studies;
            projectToUpdateDb.Mode = projectToUpdate.Mode;
            _context.Project.Update(projectToUpdateDb);
            await _context.SaveChangesAsync();
            return projectToUpdateDb;

        }

        public async Task<Project> UpdateIcon(int idProject, string iconUrl)
        {
            var project = await _context.Project.Where(pro => pro.IdProject.Equals(idProject)).FirstOrDefaultAsync();
            project.Icon = iconUrl;
            _context.Project.Update(project);
            await _context.SaveChangesAsync();
            return project;
        }

        public async Task<List<Url>> DeleteOldUrl(int idProject)
        {
            var urlProjects = await _context.Url.Where(url => url.Project.Equals(idProject)).ToListAsync();
            _context.Url.RemoveRange(urlProjects);
            await _context.SaveChangesAsync();
            return urlProjects;
        }
    }
}
