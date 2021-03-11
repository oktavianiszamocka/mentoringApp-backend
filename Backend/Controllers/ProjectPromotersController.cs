using System.Threading.Tasks;
using MentorApp.DTOs.Responses;
using MentorApp.Services;
using MentorApp.Wrappers;
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

        [HttpGet("{idProject:int}")]
        public async Task<IActionResult> GetProjectPromoters(int idProject)
        {
            var projectList = await _projectPromoterService.GetProjectPromoters(idProject);
            return Ok(new Response<ProjectPromotersDTO>(projectList));
        }
    }
}