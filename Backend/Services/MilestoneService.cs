using MentorApp.Models;
using MentorApp.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MentorApp.Services
{
    public class MilestoneService : IMilestoneService
    {
        private readonly IMilestoneRepository _milestoneRepository;

        public MilestoneService(IMilestoneRepository milestoneRepository)
        {
            _milestoneRepository = milestoneRepository;
        }
        public async Task<List<Milestone>> GetProjectMilestones(int idProject)
        {
            return await _milestoneRepository.GetProjectMilestones(idProject);
        }

        public async Task<Milestone> UpdateMilestoneToPassed(Milestone milestone)
        {
            return await _milestoneRepository.UpdateMilestoneToPassed(milestone);
        }

        public async Task<Milestone> CreateMilestone(Milestone newMilestone)
        {
            return await _milestoneRepository.CreateMilestone(newMilestone);
        }

        public async Task<Milestone> UpdateMilestone(Milestone milestone)
        {
            return await _milestoneRepository.UpdateMilestone(milestone);
        }
    }
}
