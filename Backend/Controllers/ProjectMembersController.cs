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
    [Route("api/project-members")]
    [ApiController]
    public class ProjectMembersController : ControllerBase
    {
        private readonly IProjectMemberService _projectMemberService;

        public ProjectMembersController(IProjectMemberService projectMemberService)
        {
            _projectMemberService = projectMemberService;
        }


        [HttpGet("{idProject:int}")]
        public async Task<IActionResult> GetProjectMembers(int idProject)
        {
            var projectMemberList = await _projectMemberService.GetProjectMembers(idProject);
            return Ok(new Response<List<ProjectMemberDTO>>(projectMemberList));
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewProjectMembers(NewProjectMembersDTO newMembersDTO)
        {
            try
            {
                await _projectMemberService.CreateProjectMembers(newMembersDTO);
                return StatusCode(200);
            }
            catch (HttpResponseException ex)
            {
                return StatusCode(500, ex.Value);
            }

        }
    }

}