using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MentorApp.DTOs.Responses;
using MentorApp.Models;
using Task = System.Threading.Tasks.Task;


namespace MentorApp.Repository
{
    public interface ITaskRepository
    {
        Task<List<Models.Task>> GetTasksByProject(int idProject);

        Task<Models.Task> GetTaskById(int idTask);

        Task<Models.Task> CreateNewTask(Models.Task newTask);

        Task<Models.TaskAssigning> CreateNewTaskAssigning(TaskAssigning newTaskAssigning);
        Task<List<Models.TaskStatus>> GetTaskStatus();

        Task<Models.Task> UpdateTaskStatus(Models.Task taskToUpdate);
        Task<Models.Task> UpdateTask(Models.Task taskToUpdate);
        Task<Models.Task> DeleteTask(int idTask);
        Task<Models.TaskAssigning> DeleteTaskAssigning(int idTaskAssign);
        Task<Models.TaskAssigning> GetTaskAssigningByTaskAndUser(int idTask, int idUser);
    }
}
