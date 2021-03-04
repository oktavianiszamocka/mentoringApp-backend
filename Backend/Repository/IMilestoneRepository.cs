using MentorApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MentorApp.Repository
{
    public interface IMilestoneRepository
    {
        Task<List<Milestone>> GetProjectMilestones(int IdProject);
    }
}
