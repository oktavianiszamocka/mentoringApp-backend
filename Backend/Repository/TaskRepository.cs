using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MentorApp.Models;
using MentorApp.Persistence;
using MentorApp.Repository;
using MentorApp.Wrappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Task = MentorApp.Models.Task;
using TaskStatus = MentorApp.Models.TaskStatus;

namespace MentorApp.DTOs.Responses
{
    public class TaskRepository : ITaskRepository
    {
        private readonly MentorAppContext _context;

        public TaskRepository(MentorAppContext context)
        {
            _context = context;
        }
    

        public async Task<List<Models.Task>> GetTasksByProject(int idProject)
        {
            var tasks = await _context.Task
                
                .Where(task => task.Project.Equals(idProject))
                .Include(task => task.StatusNavigation)
                .Include(task => task.TaskAssigning)
                    .ThenInclude(taskAssign => taskAssign.UserNavigation)
                .OrderBy(task => task.CreatedOn)
                .ToListAsync();

            return tasks;
        }

        public async Task<Models.Task> GetTaskById(int idTask)
        {
            return await _context.Task
                .Where(task => task.IdTask.Equals(idTask))
                .Include(task => task.StatusNavigation)
                .Include(task => task.CreatorNavigation)
                .Include(task => task.TaskAssigning)
                    .ThenInclude(taskAssign => taskAssign.UserNavigation)
                .FirstOrDefaultAsync();
        }

        public async Task<Models.Task> CreateNewTask(Models.Task newTask)
        {
            var newTaskInserted = await _context.Task.AddAsync(newTask);
            await _context.SaveChangesAsync();
            return newTaskInserted.Entity;
        }

        public async Task<TaskAssigning> CreateNewTaskAssigning(TaskAssigning newTaskAssigning)
        {
            var newTaskAssignDb = await _context.TaskAssigning.AddAsync(newTaskAssigning);
            await _context.SaveChangesAsync();
            return newTaskAssignDb.Entity;
        }

        public async Task<List<Models.TaskStatus>> GetTaskStatus()
        {
            return await _context.TaskStatus.ToListAsync();
        }

        public async Task<Models.Task> UpdateTaskStatus(Task taskToUpdate)
        {
            var taskUpdate = await _context.Task.FindAsync(taskToUpdate.IdTask);
            taskUpdate.Status = taskToUpdate.Status;
            _context.Task.Update(taskUpdate);
            await _context.SaveChangesAsync();
            return taskToUpdate;
        }

        public async Task<Models.Task> UpdateTask(Task taskToUpdate)
        {
            var taskUpdate = await _context.Task.FindAsync(taskToUpdate.IdTask);
            taskUpdate.Status = taskToUpdate.Status;
            taskUpdate.Title = taskToUpdate.Title;
            taskUpdate.Description = taskToUpdate.Description;
            taskUpdate.ExpectedEndDate = taskToUpdate.ExpectedEndDate;
            taskUpdate.StartDate = taskToUpdate.StartDate;
            taskUpdate.ActualEndDate = taskToUpdate.ActualEndDate;
            taskUpdate.Priority = taskToUpdate.Priority;
            _context.Task.Update(taskUpdate);
            await _context.SaveChangesAsync();
            return taskToUpdate;
        }

        public async Task<Models.Task> DeleteTask(int idTask)
        {
            var taskToDelete = await _context.Task.FindAsync(idTask);
            _context.Task.Remove(taskToDelete);
            await _context.SaveChangesAsync();
            return taskToDelete;
        }

        public async Task<TaskAssigning> DeleteTaskAssigning(int idTaskAssign)
        {
            var taskAssigningToDelete = await _context.TaskAssigning.FindAsync(idTaskAssign);
            _context.TaskAssigning.Remove(taskAssigningToDelete);
            await _context.SaveChangesAsync();
            return taskAssigningToDelete;
        }

        public async Task<TaskAssigning> GetTaskAssigningByTaskAndUser(int idTask, int idUser)
        {
            return await _context.TaskAssigning
                .Where(taskAssigning => taskAssigning.Task.Equals(idTask) && taskAssigning.User.Equals(idUser))
                .FirstOrDefaultAsync();
        }
    }
}
