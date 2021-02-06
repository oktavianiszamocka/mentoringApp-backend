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

        public async Task<Project> GetProjectById(int idProject)
        {
            return await _context.Project
                         .Where(project => project.IdProject.Equals(idProject))
                         .FirstOrDefaultAsync();
        }

    }
}
