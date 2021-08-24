using System.Collections.Generic;
using System.Threading.Tasks;
using MentorApp.DTOs.Requests;
using MentorApp.DTOs.Responses;
using MentorApp.Helpers;
using MentorApp.Services;
using MentorApp.Wrappers;
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

        [HttpGet("{idProject:int}")]
        public async Task<IActionResult> GetProjectPromoters(int idProject)
        {
            var projectList = await _projectPromoterService.GetProjectPromoters(idProject);
            return Ok(new Response<ProjectPromotersDTO>(projectList));
        }
        [HttpGet("{idProject:int}/email")]
        public async Task<IActionResult> GetProjectPromotersEmail(int idProject)
        {
            var projectPromoterList = await _projectPromoterService.GetAdditionalPromoterEmails(idProject);
            return Ok(new Response<List<string>>(projectPromoterList));
        }

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

        [HttpPatch]
        public async Task<IActionResult> UpdateProjectPromoter(EditProjectPromotersDTO editProjectPromotersDto)
        {
            try
            {
                var updatePromoter = await _projectPromoterService.UpdateProjectPromoter(editProjectPromotersDto);
                return StatusCode(200);
            }
            catch (HttpResponseException ex)
            {
                return StatusCode(500, ex.Value);
            }


        }
    }
}