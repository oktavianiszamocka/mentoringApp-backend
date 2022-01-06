using System;
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

        public async Task<List<Milestone>> GetProjectMilestones(int idProject)
        {
            return await _context.Milestone
                        .Where(milestone => milestone.Project.Equals(idProject))
                        .OrderBy(milestone => milestone.Sequence)
                        .ToListAsync();
        }

        public async Task<Milestone> UpdateMilestoneToPassed(Milestone milestone)
        {
            var milestoneToUpdate = await _context.Milestone.FindAsync(milestone.IdMilestone);
            milestoneToUpdate.IsDone = true;
            milestoneToUpdate.Date = DateTime.Today;
            _context.Milestone.Update(milestoneToUpdate);
            await _context.SaveChangesAsync();
            return milestoneToUpdate;
        }

        public async Task<Milestone> UpdateMilestone(Milestone milestone)
        {
            var milestoneToUpdate = await _context.Milestone.FindAsync(milestone.IdMilestone);
            milestoneToUpdate.Description = milestone.Description;
            
            _context.Milestone.Update(milestoneToUpdate);
            await _context.SaveChangesAsync();
            return milestoneToUpdate;
        }

        public async Task<Milestone> CreateMilestone(Milestone newMilestone)
        {
            var newMilestoneInserted = await _context.Milestone.AddAsync(newMilestone);
            await _context.SaveChangesAsync();
            return newMilestoneInserted.Entity;
        }

        public async Task<Milestone> GetTheLastSequenceOfProjectMilestone(int idProject)
        {
            return await _context.Milestone
                .Where(milestone => milestone.Project.Equals(idProject))
                .OrderByDescending(mil => mil.Sequence)
                .FirstOrDefaultAsync();
        }
    }
}
