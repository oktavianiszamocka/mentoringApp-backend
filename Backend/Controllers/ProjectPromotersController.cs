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
    }
}