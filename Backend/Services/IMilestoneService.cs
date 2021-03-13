using MentorApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MentorApp.Services
{
    public interface IMilestoneService
    {
        Task<List<Milestone>> GetProjectMilestones(int idProject);
        Task<Milestone> UpdateMilestoneToPassed(Milestone milestone);

        Task<Milestone> CreateMilestone(Milestone newMilestone);

        Task<Milestone> UpdateMilestone(Milestone milestone);
    }

}
