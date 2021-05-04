using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MentorApp.DTOs.Responses;
using MentorApp.Models;


namespace MentorApp.Repository
{
    public interface ITaskRepository
    {
        Task<List<Models.Task>> GetTasksByProject(int idProject);

        Task<Models.Task> GetTaskById(int idTask);

    }
}
