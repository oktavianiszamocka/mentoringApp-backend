using MentorApp.Models;
using MentorApp.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentorApp.Repository
{
    public class MilestoneRepository : IMilestoneRepository
    {
        private readonly MentorAppContext _context;

        public MilestoneRepository(MentorAppContext context)
        {
            _context = context;
        }

        public async Task<List<Milestone>> GetProjectMilestones(int IdProject)
        {
            return await _context.Milestone
                        .Where(milestone => milestone.Project.Equals(IdProject))
                        .OrderBy(milestone => milestone.Sequence)
                        .ToListAsync();
        }
    }
}
