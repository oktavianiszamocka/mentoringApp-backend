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
        public async Task<List<Milestone>> GetProjectMilestones(int IdProject)
        {
            return await _milestoneRepository.GetProjectMilestones(IdProject);
        }
    }
}
