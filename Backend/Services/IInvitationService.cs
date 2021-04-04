using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MentorApp.DTOs.Responses;
using MentorApp.Models;

namespace MentorApp.Services
{
    public interface IInvitationService
    {
        Task<List<InvitationDTO>> GetInvitationByUser(int idUser);

        Task<Invitation> CreateInvitation(Invitation newInvitation);

        Task<Invitation> UpdateInvitation(Invitation existingInvitation);
    }
}
