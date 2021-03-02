using MentorApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MentorApp.Services
{
    public interface IMilestoneService
    {
        Task<List<Milestone>> GetProjectMilestones(int IdProject);
    }
}
