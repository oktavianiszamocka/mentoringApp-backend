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

        public async Task<Task> GetTaskById(int idTask)
        {
            return await _context.Task
                .Where(task => task.IdTask.Equals(idTask))
                .Include(task => task.StatusNavigation)
                .Include(task => task.CreatorNavigation)
                .Include(task => task.TaskAssigning)
                    .ThenInclude(taskAssign => taskAssign.UserNavigation)
                .FirstOrDefaultAsync();
        }
    }
}
