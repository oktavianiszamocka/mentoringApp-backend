using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MentorApp.DTOs.Responses;
using MentorApp.Repository;
using MentorApp.Wrappers;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Routing.Template;
using Task = MentorApp.Models.Task;

namespace MentorApp.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }
        public async Task<List<TaskOverviewDTO>> GetTasksByProject(int idProject)
        {
            var taskList = await _taskRepository.GetTasksByProject(idProject);

            var taskMap = new SortedDictionary<int, List<Models.Task>>();

            foreach( var task in taskList )
            {
                if (taskMap.ContainsKey(task.Status))
                {
                    List<Models.Task> taskArr = new List<Models.Task>();
                    taskMap.TryGetValue(task.Status, out taskArr);
                    taskArr.Add(task);
                }
                else
                {
                    taskMap.Add(task.Status, new List<Task>{task});
                }
            }

            var taskDtoList = taskMap.Select(x => new TaskOverviewDTO
            {
                Status = x.Key,
                Tasks = x.Value.Select(taskProject => new TaskDTO
                {
                    IdTask = taskProject.IdTask,
                    Title = taskProject.Title,
                    StatusCode = taskProject.Status,
                    Status = taskProject.StatusNavigation.Name,
                    ExpectedEndDate = taskProject.ExpectedEndDate,
                    AssignedUserAvatars = taskProject.TaskAssigning
                        .Select(taskAssign => taskAssign.UserNavigation.Avatar).ToList()

                }).ToList()
            }).ToList();

            return taskDtoList;
        }

        public async Task<TaskDto> GetTaskById(int idTask)
        {
            var task = await _taskRepository.GetTaskById(idTask);
            TaskDto taskDto = new TaskDto
            {
                IdTask = task.IdTask,
                Title = task.Title,
                Description = task.Description,
                ExpectedEndDate = task.ExpectedEndDate,
                StartDate = task.StartDate,
                Status = task.Status,
                Project = task.Project,
                StatusName = task.StatusNavigation.Name,
                ActualEndDate =  task.ActualEndDate,
                CreatedOn = task.CreatedOn,
                Creator = task.Creator,
                CreatorUser = new UserWrapper
                {
                    IdUser = task.Creator,
                    firstName = task.CreatorNavigation.FirstName,
                    lastName = task.CreatorNavigation.LastName,
                    imageUrl = task.CreatorNavigation.Avatar
                },
                AssignedUser = task.TaskAssigning
                    .Select(taskAssign => new UserWrapper
                    {
                        IdUser = taskAssign.UserNavigation.IdUser,
                        firstName = taskAssign.UserNavigation.FirstName,
                        lastName = taskAssign.UserNavigation.LastName,
                        imageUrl = taskAssign.UserNavigation.Avatar
                    })
                    .ToList()

            };
            return taskDto;
        }
    }
}
