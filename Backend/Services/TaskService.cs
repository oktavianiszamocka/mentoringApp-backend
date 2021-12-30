using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MentorApp.DTOs.Requests;
using MentorApp.DTOs.Responses;
using MentorApp.Models;
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
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IMailService _mailService;
        private readonly IProjectRepository _projectRepository;

        public TaskService(ITaskRepository taskRepository, IMapper mapper, IMailService mailService, IUserRepository userRepository, IProjectRepository projectRepository)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
            _mailService = mailService;
            _userRepository = userRepository;
            _projectRepository = projectRepository;
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
                    Priority = taskProject.Priority,
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
                Priority = task.Priority,
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

        public async Task<List<DropdownDTO>> GetAllTaskStatus()
        {
            var taskStatus = await _taskRepository.GetTaskStatus();
            var statusDropdown = taskStatus.Select(status => new DropdownDTO
            {
                Value = status.IdStatus,
                Label = status.Name
            }).ToList();
            return statusDropdown;
        }

        public async Task<TaskDto> CreateNewTask(TaskRequestDTO newTaskRequestDto)
        {
            var newTask = _mapper.Map<Models.Task>(newTaskRequestDto);
            var newCreatedTask = await _taskRepository.CreateNewTask(newTask);
            if (newTaskRequestDto.AssignedUsers.Count > 0)
            {
                foreach (var userId in newTaskRequestDto.AssignedUsers)
                {
                    var newTaskAssign = new TaskAssigning
                    {
                        Task = newCreatedTask.IdTask,
                        User = userId
                    };
                    await _taskRepository.CreateNewTaskAssigning(newTaskAssign);
                    var project = await _projectRepository.GetProjectInfoById(newTaskRequestDto.Project);
                    var user = await _userRepository.GetUserById(userId);
                    await _mailService.AssignTaskEmail(user.FirstName, user.Email, newTaskRequestDto.Title, project.Name);

                }
            }
            var newCreatedTaskDto = await this.GetTaskById(newCreatedTask.IdTask);
            return newCreatedTaskDto;
        }

        public async Task<Task> UpdateTaskStatus(Task taskToUpdate)
        {
            return await _taskRepository.UpdateTaskStatus(taskToUpdate);
        }

        public async Task<TaskDto> UpdateTask(TaskRequestDTO taskToUpdateDTO)
        {
            var taskUpdate = _mapper.Map<Models.Task>(taskToUpdateDTO);
            var taskUpdateDB = await _taskRepository.UpdateTask(taskUpdate);
            if (taskToUpdateDTO.IsAddNewAssignee && taskToUpdateDTO.AssignedUsersToAdd.Count > 0)
            {
                foreach (var userId in taskToUpdateDTO.AssignedUsersToAdd)
                {
                    var newTaskAssign = new TaskAssigning
                    {
                        Task = taskToUpdateDTO.IdTask,
                        User = userId
                    };
                    await _taskRepository.CreateNewTaskAssigning(newTaskAssign);
                    var user = await _userRepository.GetUserById(userId);
                    var project = await _projectRepository.GetProjectInfoById(taskToUpdateDTO.Project);
                    await _mailService.AssignTaskEmail(user.FirstName, user.Email, taskToUpdateDTO.Title, project.Name);
                }
            }

            if (taskToUpdateDTO.IsRemoveAssignee && taskToUpdateDTO.AssignedUsersToRemove.Count > 0)
            {
                foreach (var user in taskToUpdateDTO.AssignedUsersToRemove)
                {
                    var taskAssigningToRemove =
                        await _taskRepository.GetTaskAssigningByTaskAndUser(taskToUpdateDTO.IdTask, user);
                    await _taskRepository.DeleteTaskAssigning(taskAssigningToRemove.IdAssign);
                }
            }
            return await this.GetTaskById(taskToUpdateDTO.IdTask);

        }

        public async Task<Task> DeleteTask(int idTask)
        {
            return await _taskRepository.DeleteTask(idTask);
        }
    }
}
