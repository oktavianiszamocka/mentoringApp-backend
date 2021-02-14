using MentorApp.Models;
using MentorApp.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
    }
}
