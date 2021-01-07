﻿using MentorApp.Models;
using MentorApp.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentorApp.Repository
{
    public class ProjectMemberRepository : IProjectMemberRepository
    {
        private readonly MentorAppContext _context;

        public ProjectMemberRepository(MentorAppContext context)
        {
            _context = context;
        }
        public async Task<List<Project>> GetProjectName(int IdUser)
        {
            return await _context.ProjectMembers
                        .Include(project => project.ProjectNavigation)
                        .ThenInclude(superVisor => superVisor.SuperviserNavigation)
                        .Where(project => project.Member.Equals(IdUser))
                        .Select(project => project.ProjectNavigation)
                    //    .Select(project => project.ProjectNavigation.Name)
                        .ToListAsync();
        }
    }
}
