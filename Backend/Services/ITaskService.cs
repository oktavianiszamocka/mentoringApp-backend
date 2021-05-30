using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MentorApp.DTOs.Responses;

namespace MentorApp.Services
{
    public interface ITaskService
    {
        Task<List<TaskOverviewDTO>> GetTasksByProject(int idProject);
        Task<TaskDto> GetTaskById(int idTask);
    }
}
