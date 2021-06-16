using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MentorApp.DTOs.Requests;
using MentorApp.DTOs.Responses;

namespace MentorApp.Services
{
    public interface ITaskService
    {
        Task<List<TaskOverviewDTO>> GetTasksByProject(int idProject);
        Task<TaskDto> GetTaskById(int idTask);

        Task<List<DropdownDTO>> GetAllTaskStatus();
        Task<TaskDto> CreateNewTask(TaskRequestDTO newTaskRequestDto);
        Task<Models.Task> UpdateTaskStatus(Models.Task taskToUpdate);
        Task<TaskDto> UpdateTask(TaskRequestDTO taskToUpdateDTO);

        Task<Models.Task> DeleteTask(int idTask);
    }
}
