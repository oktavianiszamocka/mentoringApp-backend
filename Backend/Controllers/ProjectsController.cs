using MentorApp.DTOs.Responses;
using MentorApp.Filter;
using MentorApp.Helpers;
using MentorApp.Models;
using MentorApp.Persistence;
using MentorApp.Services;
using MentorApp.Wrappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentorApp.Controllers
{
    [Route("api/projects")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private const int DefaultPageSize = 5;
        private readonly IProjectMemberService _projectMemberService;
        private readonly IUriService _uriService;
        private readonly IProjectService _projectService;
        

        public ProjectsController(IProjectMemberService projectMemberService, IUriService uriService, IProjectService projectService)
        {
            _projectMemberService = projectMemberService;
            _uriService = uriService;
            _projectService = projectService;
        }

        [HttpGet("{IdUser:int}")]
        public async Task<IActionResult> GetUserProjectsName(int IdUser)
        {
            var projectList = await _projectMemberService.GetProjectsNameByIdUser(IdUser);
            return Ok(new Response<List<ProjectDTO>>(projectList));
        }

        [HttpGet("userProjects/{IdUser:int}")]
        public async Task<IActionResult> GetUserProjects([FromQuery] Filter.PaginationFilter filter, int IdUser)
        {
            var route = Request.Path.Value;
            var validFilter = new PaginationFilter(filter.PageNumber, DefaultPageSize);
            var projectList = await _projectMemberService.GetProjectsByIdUser(IdUser);
            var projectsListWithPaging = projectList
                            .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                            .Take(validFilter.PageSize)
                            .ToList();

            var totalRecords = projectList.Count();

            var pagedResponse = PaginationHelper.CreatePagedReponse<ProjectWrapper>(projectsListWithPaging, validFilter, totalRecords, _uriService, route);

            return Ok(pagedResponse);
        }

        [HttpGet("userProjects/{IdUser:int}/search")]
        public async Task<IActionResult> GetUserProjectsBySearchName([FromQuery] Filter.PaginationFilter filter, int IdUser, [FromQuery(Name = "projectName")]String SearchName)
        {
            var route = Request.Path.Value;
            var validFilter = new PaginationFilter(filter.PageNumber, DefaultPageSize);
            var projectList = await _projectMemberService.GetProjectByNameSearch(IdUser, SearchName);
            var projectsListWithPaging = projectList
                            .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                            .Take(validFilter.PageSize)
                            .ToList();

            var totalRecords = projectList.Count();

            var pagedResponse = PaginationHelper.CreatePagedReponse<ProjectWrapper>(projectsListWithPaging, validFilter, totalRecords, _uriService, route);

            return Ok(pagedResponse);
        }


        [HttpGet("members/{IdProject:int}")]
        public async Task<IActionResult> GetProjectMembers(int IdProject)
        {
            var projectMemberList = await _projectMemberService.GetProjectMembers(IdProject);
            return Ok(new Response<List<ProjectMemberDTO>>(projectMemberList));
        }

        [HttpGet("projectInfo/{IdProject:int}")]
        public async Task<IActionResult> GetProjectsInfo(int IdProject)
        {
            var project = await _projectService.GetProjectById(IdProject);
            return Ok(new Response<Project>(project));
        }

    }
}