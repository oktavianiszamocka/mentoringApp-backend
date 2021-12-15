using System.Collections.Generic;
using System.Threading.Tasks;
using MentorApp.DTOs.Requests;
using MentorApp.DTOs.Responses;
using MentorApp.Helpers;
using MentorApp.Services;
using MentorApp.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MentorApp.Controllers
{
    [Route("api/project-promoters")]
    [ApiController]
    public class ProjectPromotersController : ControllerBase
    {
        private readonly IProjectPromoterService _projectPromoterService;

        public ProjectPromotersController(IProjectPromoterService projectPromoterService)
        {
            _projectPromoterService = projectPromoterService;
        }

        [Authorize]
        [HttpGet("{idProject:int}")]
        public async Task<IActionResult> GetProjectPromoters(int idProject)
        {
            var projectList = await _projectPromoterService.GetProjectPromoters(idProject);
            return Ok(new Response<ProjectPromotersDTO>(projectList));
        }

        [Authorize]
        [HttpGet("{idProject:int}/email")]
        public async Task<IActionResult> GetProjectPromotersEmail(int idProject)
        {
            var projectPromoterList = await _projectPromoterService.GetAdditionalPromoterEmails(idProject);
            return Ok(new Response<List<string>>(projectPromoterList));
        }

        [Authorize(Roles = "mentor")]
        [HttpPost]
        public async Task<IActionResult> CreateNewProjectPromoter(NewSupervisorsProjectDTO newSupervisorsProject)
        {
            try
            {
                await _projectPromoterService.CreateProjectPromoter(newSupervisorsProject);
                return StatusCode(200);
            }
            catch (HttpResponseException ex)
            {
                return StatusCode(500, ex.Value);
            }
        }

        [Authorize(Roles = "mentor")]
        [HttpDelete("{idProject:int}")]
        public async Task<IActionResult> DeleteMeeting(int idProject, [FromQuery(Name = "email")] string userEmail)
        {
            await _projectPromoterService.DeleteProjectPromoter(idProject, userEmail);
            return StatusCode(200);
        }
    }
}