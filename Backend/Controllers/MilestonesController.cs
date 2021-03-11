using System.Collections.Generic;
using System.Threading.Tasks;
using MentorApp.Models;
using MentorApp.Services;
using MentorApp.Wrappers;
using Microsoft.AspNetCore.Mvc;

namespace MentorApp.Controllers
{
    [Route("api/milestones")]
    [ApiController]
    public class MilestonesController : ControllerBase
    {
        private readonly IMilestoneService _milestoneService;

        public MilestonesController(IMilestoneService milestoneService)
        {
            _milestoneService = milestoneService;
        }

        [HttpGet("{idProject:int}")]
        public async Task<IActionResult> GetProjectMilestones(int idProject)
        {
            var milestones = await _milestoneService.GetProjectMilestones(idProject);
            return Ok(new Response<List<Milestone>>(milestones));
        }
    }
}