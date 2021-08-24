using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MentorApp.Models;
using MentorApp.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MentorApp.Repository
{
    public class InvitationRepository : IInvitationRepository{

        private readonly MentorAppContext _context;

        public InvitationRepository(MentorAppContext context)
        {
            _context = context;
        }
        public async Task<List<Invitation>> GetInvitationByUser(int idUser)
        {
            return await _context.Invitation
                .Where(invitation => invitation.For_Who.Equals(idUser) && invitation.IsActive == true)
                .Include(invitation => invitation.ProjectNavigation)
                    .ThenInclude(project => project.SuperviserNavigation)
                .Include(invitation => invitation.MemberRoleNavigation)
                .OrderBy(invitation => invitation.IdInvitation)
                .ToListAsync();
        }

        public async Task<Invitation> CreateInvitation(Invitation newInvitation)
        {
            var newInvitationInserted = await _context.Invitation.AddAsync(newInvitation);
            await _context.SaveChangesAsync();
            return newInvitationInserted.Entity;


        }

        public async Task<List<Invitation>> CreateManyInvitations(List<Invitation> newInvitations)
        { 
            await _context.Invitation.AddRangeAsync(newInvitations);
            await _context.SaveChangesAsync();
            return newInvitations;
        }

        public async Task<Invitation> UpdateInvitation(Invitation existingInvitation)
        {
            var existingInvitationDB = await _context.Invitation.FindAsync(existingInvitation.IdInvitation);
            existingInvitationDB.IsAccepted = existingInvitation.IsAccepted;
            existingInvitationDB.IsActive = false;
            _context.Invitation.Update(existingInvitationDB);
            await _context.SaveChangesAsync();
            return existingInvitationDB;
        }

        public async Task<bool> IsProjectMemberLeaderInvitationExist(int idProject)
        {
            var projectLeaderInvitation = await _context.Invitation
                .Where(invitation => invitation.IsActive && invitation.IsMemberInvitation && invitation.Role.Equals(1))
                .FirstOrDefaultAsync();
            if (projectLeaderInvitation != null)
            {
                return true;
            }

            return false;
        }
    }
}
