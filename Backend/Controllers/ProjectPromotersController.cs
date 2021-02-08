using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MentorApp.DTOs.Responses;
using MentorApp.Services;
using MentorApp.Wrappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MentorApp.Controllers
{
    [Route("api/projectpromoters")]
    [ApiController]
    public class ProjectPromotersController : ControllerBase
    {
        private readonly IProjectPromoterService _projectPromoterService;

        public ProjectPromotersController(IProjectPromoterService projectPromoterService)
        {
            _projectPromoterService = projectPromoterService;
        }

        [HttpGet("{IdProject:int}")]
        public async Task<IActionResult> GetProjectPromoters(int IdProject)
        {
            var projectList = await _projectPromoterService.GetProjectPromoters(IdProject);
            return Ok(new Response<ProjectPromotersDTO>(projectList));
        }
    }
}