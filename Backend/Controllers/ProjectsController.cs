using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MentorApp.DTOs.Requests;
using MentorApp.DTOs.Responses;
using MentorApp.Filter;
using MentorApp.Helpers;
using MentorApp.Services;
using MentorApp.Wrappers;
using Microsoft.AspNetCore.Mvc;

namespace MentorApp.Controllers
{
    [Route("api/projects")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private const int DefaultPageSize = 5;
        private readonly IProjectMemberService _projectMemberService;
        private readonly IProjectService _projectService;
        private readonly IUriService _uriService;


        public ProjectsController(IProjectMemberService projectMemberService, IUriService uriService,
            IProjectService projectService)
        {
            _projectMemberService = projectMemberService;
            _uriService = uriService;
            _projectService = projectService;
        }

        [HttpGet("{idUser:int}")]
        public async Task<IActionResult> GetUserProjectsName(int IdUser)
        {
            var projectList = await _projectMemberService.GetProjectsNameByIdUser(IdUser);
            return Ok(new Response<List<ProjectDTO>>(projectList));
        }

        [HttpGet("userProjects/{idUser:int}")]
        public async Task<IActionResult> GetUserProjects([FromQuery] PaginationFilter filter, int idUser)
        {
            var route = Request.Path.Value;
            var validFilter = new PaginationFilter(filter.PageNumber, DefaultPageSize);
            var projectList = await _projectMemberService.GetProjectsByIdUser(idUser);
            var projectsListWithPaging = projectList
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToList();

            var totalRecords = projectList.Count();

            var pagedResponse = PaginationHelper.CreatePagedReponse(projectsListWithPaging, validFilter, totalRecords,
                _uriService, route);

            return Ok(pagedResponse);
        }

        [HttpGet("userProjects/{idUser:int}/search")]
        public async Task<IActionResult> GetUserProjectsBySearchName([FromQuery] PaginationFilter filter, int IdUser,
            [FromQuery(Name = "projectName")] string SearchName)
        {
            var route = Request.Path.Value;
            var validFilter = new PaginationFilter(filter.PageNumber, DefaultPageSize);
            var projectList = await _projectMemberService.GetProjectByNameSearch(IdUser, SearchName);
            var projectsListWithPaging = projectList
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToList();

            var totalRecords = projectList.Count();

            var pagedResponse = PaginationHelper.CreatePagedReponse(projectsListWithPaging, validFilter, totalRecords,
                _uriService, route);

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
            var project = await _projectService.GetProjectInfoById(IdProject);
            return Ok(new Response<ProjectInfoDTO>(project));
        }
    }
}