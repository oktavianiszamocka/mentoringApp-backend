﻿using MentorApp.DTOs.Responses;
using MentorApp.Models;
using MentorApp.Persistence;
using MentorApp.Services;
using MentorApp.Wrappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentorApp.Controllers
{
    [Route("api/projects")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectMemberService _projectMemberService;

       public ProjectsController(IProjectMemberService projectMemberService)
        {
            _projectMemberService = projectMemberService;
        }

        [HttpGet("{IdUser:int}")]
        public async Task<IActionResult> GetUserProjectsName(int IdUser)
        {
            var projectList = await _projectMemberService.GetProjectsNameByIdUser(IdUser);
            return Ok(new Response<List<ProjectDTO>>(projectList));
        }

        [HttpGet("userProjects/{IdUser:int}")]
        public async Task<IActionResult> GetUserProjects(int IdUser)
        {
            var projectList = await _projectMemberService.GetProjectsByIdUser(IdUser);
            return Ok(new Response<List<ProjectWrapper>>(projectList));
        }
    }
}