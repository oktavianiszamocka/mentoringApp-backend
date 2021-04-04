using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MentorApp.DTOs.Responses;
using MentorApp.Models;
using MentorApp.Repository;

namespace MentorApp.Services
{
    public class InvitationService : IInvitationService
    {
        private readonly IInvitationRepository _invitationRepository;
        private readonly IProjectPromoterService _projectPromoterService;
        private readonly IProjectMemberService _projectMemberService;

        public InvitationService(IInvitationRepository invitationRepository, IProjectPromoterService projectPromoterService, IProjectMemberService projectMemberService)
        {
            _invitationRepository = invitationRepository;
            _projectPromoterService = projectPromoterService;
            _projectMemberService = projectMemberService;
        }
        public async Task<List<InvitationDTO>> GetInvitationByUser(int idUser)
        {
            var invitations =  await _invitationRepository.GetInvitationByUser(idUser);
            var invitationsDTO = ConvertToInvitationDtos(invitations);
            return invitationsDTO;
        }

        public async Task<Invitation> CreateInvitation(Invitation newInvitation)
        {
            return await _invitationRepository.CreateInvitation(newInvitation);
        }

        public async Task<Invitation> UpdateInvitation(Invitation existingInvitation)
        {
            var updatedInvitation = await _invitationRepository.UpdateInvitation(existingInvitation);
            if (updatedInvitation.IsAccepted)
            {
                if (updatedInvitation.IsMemberInvitation)
                {
                    await _projectMemberService.InsertProjectMember(updatedInvitation);
                }
                else
                {
                    await _projectPromoterService.InsertProjectPromoter(updatedInvitation);
                }

            }
           
            return updatedInvitation;
        }

        public List<InvitationDTO> ConvertToInvitationDtos(List<Invitation> invitations)
        {
            var invitationDtos = invitations
                .Select(inv => new InvitationDTO
                {
                    IdInvitation = inv.IdInvitation,
                    IdProject = inv.Project,
                    For_Who = inv.For_Who,
                    ProjectName = inv.ProjectNavigation.Name,
                    ProjectOwnerName = inv.ProjectNavigation.SuperviserNavigation.FirstName + " " +
                                       inv.ProjectNavigation.SuperviserNavigation.LastName,
                    Role = (inv.IsMemberInvitation != false) ? inv.Role : null,
                    RoleName = (inv.IsMemberInvitation != false) ? inv.MemberRoleNavigation.Role : null,
                    IsMemberInvitation = inv.IsMemberInvitation,
                    IsAccepted = inv.IsMemberInvitation,
                    IsActive = true

                }).ToList();

            return invitationDtos;
        }
    }
}
