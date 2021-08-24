using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MentorApp.Models;
using Task = System.Threading.Tasks.Task;

namespace MentorApp.Repository
{
    public interface IInvitationRepository
    {
        Task<List<Invitation>> GetInvitationByUser(int idUser);

        Task<Invitation> CreateInvitation(Invitation newInvitation);
        Task<List<Invitation>> CreateManyInvitations(List<Invitation> newInvitations);

        Task<Invitation> UpdateInvitation(Invitation existingInvitation);

        Task<Boolean> IsProjectMemberLeaderInvitationExist(int idProject);
    }
}
