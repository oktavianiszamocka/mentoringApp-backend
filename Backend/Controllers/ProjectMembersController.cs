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
    [Route("api/projectMembers")]
    [ApiController]
    public class ProjectMembersController : ControllerBase
    {
        private readonly IProjectMemberService _projectMemberService;

        public ProjectMembersController(IProjectMemberService projectMemberService)
        {
            _projectMemberService = projectMemberService;
        }


        [HttpGet("{IdProject:int}")]
        public async Task<IActionResult> GetProjectMembers(int IdProject)
        {
            var projectMemberList = await _projectMemberService.GetProjectMembers(IdProject);
            return Ok(new Response<List<ProjectMemberDTO>>(projectMemberList));
        }

    }
}