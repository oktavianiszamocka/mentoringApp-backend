using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MentorApp.Models;
using MentorApp.Services;
using MentorApp.Wrappers;
using Microsoft.AspNetCore.Http;
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

        [HttpGet("{IdProject:int}")]
        public async Task<IActionResult> GetProjectMilestones(int IdProject)
        {
            var milestones = await _milestoneService.GetProjectMilestones(IdProject);
            return Ok(new Response<List<Milestone>>(milestones));
        }
    }
}