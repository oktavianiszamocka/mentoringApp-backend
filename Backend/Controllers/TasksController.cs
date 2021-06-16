using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MentorApp.DTOs.Requests;
using MentorApp.DTOs.Responses;
using MentorApp.Helpers;
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

        [HttpGet("status")]
        public async Task<IActionResult> GetTaskStatus()
        {
            var taskStatus = await _taskService.GetAllTaskStatus();
            return Ok(new Response<List<DropdownDTO>>(taskStatus));
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewTask(TaskRequestDTO newTask)
        {
            try
            {
                var newTaskDto = await _taskService.CreateNewTask(newTask);
                return StatusCode(201, newTaskDto);
            }
            catch (HttpResponseException ex)
            {
                return StatusCode(500, ex.Value);
            }
        }

        [HttpPatch("update-status")]
        public async Task<IActionResult> UpdateTaskStatus(Models.Task taskToUpdate)
        {
            var updatedTask = await _taskService.UpdateTaskStatus(taskToUpdate);
            return StatusCode(200, updatedTask);
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateTask(TaskRequestDTO taskToUpdate)
        {
            var updatedTask = await _taskService.UpdateTask(taskToUpdate);
            return StatusCode(200, updatedTask);
        }

        [HttpDelete("{idTask:int}")]
        public async Task<IActionResult> DeleteTask(int idTask)
        {
            await _taskService.DeleteTask(idTask);
            return StatusCode(200);
        }

    }
}