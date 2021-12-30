using System.Collections.Generic;
using MentorApp.Models;
using MentorApp.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Amazon.Runtime.SharedInterfaces;

namespace MentorApp.Repository
{
    public class ProjectPromotersRepository : IProjectPromotersRepository
    {
        private readonly MentorAppContext _context;

        public ProjectPromotersRepository(MentorAppContext context)
        {
            _context = context;

        }

        public async Task<Project> GetProjectPromoters(int IdProject)
        {
            return await _context.Project
                            .Where(project => project.IdProject.Equals(IdProject))
                            .Include(project => project.SuperviserNavigation)
                            .Include(project => project.ProjectPromoter)

                                .ThenInclude(promoter => promoter.UserNavigation)
                            .FirstOrDefaultAsync();

        }

        public async Task<List<Project>> GetPromotorProjects(int idUser)
        {
            var myProject = await _context.ProjectPromoter
                .Where(project => project.User.Equals(idUser))
                .Include(project => project.ProjectNavigation)
                .Include(project => project.ProjectNavigation.StatusNavigation)
                .Include(project => project.ProjectNavigation.SuperviserNavigation)
                .Include(project => project.ProjectNavigation.ModeNavigation)
                .Include(project => project.ProjectNavigation.StudiesNavigation)
                .Select(project => project.ProjectNavigation)
                .ToListAsync();

            return myProject;


        }


        public async Task<User> GetProjectPromoterByEmail(string emailUser)
        {
            return await _context.User
                        .Where(user => user.Email.Equals(emailUser) && user.Role == 3)
                        .FirstOrDefaultAsync();
        }

        public async Task<ProjectPromoter> GetProjectPromoterByIdProjectAndIdUser(int idProject, int idUser)
        {
            return await _context.ProjectPromoter
                .Where(promoter => promoter.Project.Equals(idProject) && promoter.User.Equals(idUser))
                .FirstOrDefaultAsync();
        }

        public async Task<ProjectPromoter> CreateProjectPromoter(ProjectPromoter promoter)
        {
            var newPromoter = await _context.ProjectPromoter.AddAsync(promoter);
            await _context.SaveChangesAsync();
            return newPromoter.Entity;

        }

        public async Task<ProjectPromoter> DeleteProjectPromoter(int idProjectPromoter)
        {
            var toRemove = await _context.ProjectPromoter.FindAsync(idProjectPromoter);
            _context.ProjectPromoter.Remove(toRemove);
            await _context.SaveChangesAsync(); 
            return toRemove;
        }

        public async Task<List<ProjectPromoter>> GetAdditionalPromoters(int idProject)
        {
            return await _context.ProjectPromoter
                .Where(promoter => promoter.Project.Equals(idProject))
                .Include(promoter => promoter.UserNavigation)
                .ToListAsync();
        }
    }
}
