using System.Collections.Generic;
using System.Threading.Tasks;
using MentorApp.DTOs.Requests;
using MentorApp.DTOs.Responses;
using MentorApp.Helpers;
using MentorApp.Models;
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

        [HttpGet("roles")]
        public async Task<IActionResult> GetMemberRoles()
        {
            var roleList = await _projectMemberService.GetMemberRoles();
            return Ok(new Response<List<DropdownDTO>>(roleList));
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewProjectMembers(NewProjectMembersDTO newMembersDTO)
        {
            try
            {
                var newProjectMembers = await _projectMemberService.CreateProjectMembers(newMembersDTO);
                return StatusCode(200, newProjectMembers);
            }
            catch (HttpResponseException ex)
            {
                return StatusCode(500, ex.Value);
            }

        }

        [HttpPatch]
        public async Task<IActionResult> UpdateProjectMember(EditProjectMember editProjectMember)
        {
            try
            {
                var updateProjectMember = await _projectMemberService.UpdateProjectMember(editProjectMember);
                return StatusCode(200, updateProjectMember);

            }
            catch (HttpResponseException ex)
            {
                return StatusCode(500, ex.Value);
            }
        }
    }

}