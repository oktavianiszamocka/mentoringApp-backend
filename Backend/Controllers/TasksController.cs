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
    [Route("api/tasks")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet("{idProject:int}")]
        public async Task<IActionResult> GetProjectTasks(int idProject)
        {
            var tasks = await _taskService.GetTasksByProject(idProject);
            return Ok(new Response<List<TaskOverviewDTO>>(tasks));
        }

        [HttpGet("detail/{idTask:int}")]
        public async Task<IActionResult> GetTaskById(int idTask)
        {
            var task = await _taskService.GetTaskById(idTask);
            return Ok(new Response<TaskDto>(task));
        }
    }
}