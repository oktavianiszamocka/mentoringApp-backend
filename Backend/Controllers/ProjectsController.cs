using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MentorApp.DTOs.Requests;
using MentorApp.DTOs.Responses;
using MentorApp.Filter;
using MentorApp.Helpers;
using MentorApp.Models;
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
        public async Task<IActionResult> GetUserProjectsName(int idUser)
        {
            var projectList = await _projectMemberService.GetProjectsNameByIdUser(idUser);
            return Ok(new Response<List<ProjectDTO>>(projectList));
        }

        [HttpGet("user-projects/{idUser:int}")]
        public async Task<IActionResult> GetUserProjects([FromQuery] PaginationFilter filter, int idUser)
        {
            var route = Request.Path.Value;
            var validFilter = new PaginationFilter(filter.PageNumber, DefaultPageSize);
            var projectList = await _projectMemberService.GetMyProjectFiltered(idUser, "", null, null);
            var projectsListWithPaging = projectList
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToList();

            var totalRecords = projectList.Count();

            var pagedResponse = PaginationHelper.CreatePagedReponse(projectsListWithPaging, validFilter, totalRecords,
                _uriService, route);

            return Ok(pagedResponse);
        }

        [HttpGet("user-projects/{idUser:int}/search")]
        public async Task<IActionResult> GetUserProjectsBySearchName([FromQuery] PaginationFilter filter, int idUser,
            [FromQuery(Name = "projectName")] string SearchName, [FromQuery(Name = "study")] int? study, [FromQuery(Name = "mode")] int mode)
        {
            try
            { 
                var route = Request.Path.Value;
                var validFilter = new PaginationFilter(filter.PageNumber, DefaultPageSize);
                var projectList = await _projectMemberService.GetMyProjectFiltered(idUser, SearchName, study, mode);
                var projectsListWithPaging = projectList
                    .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                    .Take(validFilter.PageSize)
                    .ToList();

                var totalRecords = projectList.Count();

                var pagedResponse = PaginationHelper.CreatePagedReponse(projectsListWithPaging, validFilter, totalRecords,
                    _uriService, route);

                return Ok(pagedResponse);
            }
            catch (HttpResponseException ex)
            {
                return StatusCode(500, ex.Value);
            }

        }


        [HttpGet("members/{idProject:int}")]
        public async Task<IActionResult> GetProjectMembers(int idProject)
        {
            var projectMemberList = await _projectMemberService.GetProjectMembers(idProject);
            return Ok(new Response<List<ProjectMemberDTO>>(projectMemberList));
        }

        [HttpGet("project-info/{idProject:int}")]
        public async Task<IActionResult> GetProjectsInfo(int idProject)
        {
            var project = await _projectService.GetProjectInfoById(idProject);
            return Ok(new Response<ProjectInfoDTO>(project));
        }

        [HttpGet("status")]
        public async Task<IActionResult> GetAllProjectStatus()
        {
            var allProjectStatus = await _projectService.GetAllProjectStatus();
            return Ok(new Response<List<DropdownDTO>>(allProjectStatus));
        }

        [HttpGet("studies")]
        public async Task<IActionResult> GetAllProjectStudies()
        {
            var allProjectStudies = await _projectService.GetAllProjectStudies();
            return Ok(new Response<List<DropdownDTO>>(allProjectStudies));
        }

        [HttpGet("mode")]
        public async Task<IActionResult> GetAllProjectModes()
        {
            var allProjectModes = await _projectService.GetAllProjectMode();
            return Ok(new Response<List<DropdownDTO>>(allProjectModes));
        }

        [HttpGet("url-types")]
        public async Task<IActionResult> GetUrlTypes()
        {
            var allProjectUrlTypes = await _projectService.GetAllUrlType();
            return Ok(new Response<List<DropdownDTO>>(allProjectUrlTypes));
        }

        [HttpGet("project-urls/{idProject:int}")]
        public async Task<IActionResult> GetProjectUrls(int idProject)
        {
            var allProjectUrls = await _projectService.GetAllProjectUrls(idProject);
            return Ok(new Response<List<Url>>(allProjectUrls));
        }


        //mentor
        [HttpPost]
        public async Task<IActionResult> SaveNewProject(NewProjectDTO projectDTO)
        {
            try
            {
                var newProject = await _projectService.SaveNewProject(projectDTO);
                return StatusCode(201, newProject);
            }
            catch (HttpResponseException ex)
            {
                return StatusCode(500, ex.Value);
            }
        }

        //mentor
        [HttpPatch]
        public async Task<IActionResult> UpdateProject(Project updateProject)
        {
            var updatedProject = await _projectService.UpdateProject(updateProject);
            return StatusCode(200, updatedProject);
        }
        //mentor
        [HttpPatch("project-icon")]
        public async Task<IActionResult> UpdateIcon([FromQuery(Name = "project")]int idProject, [FromQuery(Name = "icon")]  String pictureUrl)
        {
            var pro = await _projectService.UpdateIcon(idProject, pictureUrl);
            return StatusCode(200, pro);
        }

        [HttpPatch("project-urls")]
        public async Task<IActionResult> UpdateProjectUrls( List<Models.Url> projectUrls)
        {
            var pro = await _projectService.SaveNewProjectUrl( projectUrls);
            return StatusCode(200, pro);
        }
    }
}