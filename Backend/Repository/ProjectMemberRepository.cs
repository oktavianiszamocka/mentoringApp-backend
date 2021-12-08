using MentorApp.Models;
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

        public async Task<List<Project>> GetMyProjectFiltered(int IdUser)
        {
            return await _context.ProjectMembers
                    .Include(project => project.ProjectNavigation)
                    .Include(project => project.ProjectNavigation.StatusNavigation)
                    .Include(project => project.ProjectNavigation.SuperviserNavigation)
                    .Include(project => project.ProjectNavigation.ModeNavigation)
                    .Include(project => project.ProjectNavigation.StudiesNavigation)
                    .Where(project => project.Member.Equals(IdUser) ) 
                    .Select(project => project.ProjectNavigation)
                    .ToListAsync();

        }


        public async Task<List<Project>> GetProjectName(int IdUser)
        {
            return await _context.ProjectMembers
                        .Include(project => project.ProjectNavigation)
                        .Where(project => project.Member.Equals(IdUser))
                        .Select(project => project.ProjectNavigation)
                        .ToListAsync();
        }

        public async Task<List<ProjectMembers>> GetProjectMembers(int IdProject)
        {
            return await _context.ProjectMembers
                        .Include(member => member.MemberRoleNavigation)
                        .Include(project => project.MemberNavigation)
                        .ThenInclude(user => user.Profile)
                        .Where(project => project.Project.Equals(IdProject))
                        .OrderBy(member => member.Role)
                        .ToListAsync();

        }

        public async Task<User> GetStudentByEmail(string emailUser)
        {
            return await _context.User
                .Where(user => user.Email.Equals(emailUser) && user.Role == 2)
                .FirstOrDefaultAsync();
        }

        public async Task<ProjectMembers> CreateNewProjectMember(ProjectMembers newMember)
        {
            var insertedNewMember = await _context.ProjectMembers.AddAsync(newMember);
            await _context.SaveChangesAsync();
            return insertedNewMember.Entity;
        }

        public async Task<List<MemberRole>> GetMemberRoles()
        {
            return await _context.MemberRole.ToListAsync();
        }

        public async Task<bool> IsProjectLeaderExistInProject(int idProject)
        {
            var projectLeader = await _context.ProjectMembers
                .Where(member => member.Project.Equals(idProject) && member.Role.Equals(1))
                .FirstOrDefaultAsync();

            if (projectLeader != null)
            {
                return true;
            }

            return false;
        }

        public async Task<ProjectMembers> RemoveProjectMember(int idProjectMember)
        {
            var removeProjectMember = await _context.ProjectMembers.FindAsync(idProjectMember);
            _context.Remove(removeProjectMember);
            await _context.SaveChangesAsync();
            return removeProjectMember;
        }

        public async Task<ProjectMembers> UpdateProjectRole(int idProjectMember, int newRole)
        {
            var updateProjectMember = await _context.ProjectMembers.FindAsync(idProjectMember);
            updateProjectMember.Role = newRole;
            _context.ProjectMembers.Update(updateProjectMember);
            await _context.SaveChangesAsync();
            return updateProjectMember;
        }
    }
}
