using MentorApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentorApp.Services
{
    public interface IMilestoneService
    {
        Task<List<Milestone>> GetProjectMilestones(int IdProject);
    }
}
