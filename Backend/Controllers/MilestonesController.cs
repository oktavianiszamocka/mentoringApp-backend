using System.Collections.Generic;
using System.Threading.Tasks;
using MentorApp.Models;
using MentorApp.Services;
using MentorApp.Wrappers;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        [HttpGet("{idProject:int}")]
        public async Task<IActionResult> GetProjectMilestones(int idProject)
        {
            var milestones = await _milestoneService.GetProjectMilestones(idProject);
            return Ok(new Response<List<Milestone>>(milestones));
        }

        [Authorize(Roles = "mentor")]
        [HttpPost]
        public async Task<IActionResult> SaveNewMilestone(Milestone newMilestone)
        {
            var newMilestoneInserted = await _milestoneService.CreateMilestone(newMilestone);
            return StatusCode(201, newMilestoneInserted);
        }
        [Authorize(Roles = "mentor")]
        [HttpPatch("update-step")]
        public async Task<IActionResult> UpdateMilestoneToPassed(Milestone milestone)
        {
            var updatedMilestone = await _milestoneService.UpdateMilestoneToPassed(milestone);
            return StatusCode(200, updatedMilestone);
        }
        [Authorize(Roles = "mentor")]
        [HttpPatch]
        public async Task<IActionResult> UpdateMilestone(Milestone milestone)
        {
            var updatedMilestone = await _milestoneService.UpdateMilestone(milestone);
            return StatusCode(200, updatedMilestone);
        }
    }

}