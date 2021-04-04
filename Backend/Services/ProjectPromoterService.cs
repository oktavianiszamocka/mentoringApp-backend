using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using MentorApp.DTOs.Responses;
using MentorApp.Repository;
using MentorApp.Wrappers;
using System.Linq;
using System.Threading.Tasks;
using MentorApp.DTOs.Requests;
using MentorApp.Helpers;
using MentorApp.Models;

namespace MentorApp.Services
{
    public class ProjectPromoterService : IProjectPromoterService
    {

        private readonly IProjectPromotersRepository _projectPromotersRepository;
        private readonly IInvitationRepository _invitationRepository;

        public ProjectPromoterService(IProjectPromotersRepository projectPromotersRepository, IInvitationRepository invitationRepository)
        {
            _projectPromotersRepository = projectPromotersRepository;
            _invitationRepository = invitationRepository;
        }

        public async Task<ProjectPromotersDTO> GetProjectPromoters(int IdProject)
        {
            var projectDB = await _projectPromotersRepository.GetProjectPromoters(IdProject);

            var projectPromoterDTO = new ProjectPromotersDTO
            {
                MainMentor = new UserWrapper
                {
                    IdUser = (int)projectDB.Superviser,
                    firstName = projectDB.SuperviserNavigation.FirstName,
                    lastName = projectDB.SuperviserNavigation.LastName,
                    imageUrl = projectDB.SuperviserNavigation.Avatar
                },
                AdditionalMentors = projectDB.ProjectPromoter
                                    .Where(promoter => !promoter.User.Equals(projectDB.Superviser))
                                    .Select(promoter => new UserWrapper
                                    {
                                        IdUser = promoter.UserNavigation.IdUser,
                                        firstName = promoter.UserNavigation.FirstName,
                                        lastName = promoter.UserNavigation.LastName,
                                        imageUrl = promoter.UserNavigation.Avatar
                                    }).ToList()

            };



            return projectPromoterDTO;
        }

        public async Task<NewSupervisorsProjectDTO> CreateProjectPromoter(NewSupervisorsProjectDTO newSupervisors)
        {
            List<Invitation> invitationsToInsert = new List<Invitation>();
            if (newSupervisors.SupervisorEmails.Count > 0)
            {
                for (int i = 0; i < newSupervisors.SupervisorEmails.Count; i++)
                {
                    await searchSupervisorUserAndCreateInvitation(newSupervisors.SupervisorEmails[i], i + 1,
                        newSupervisors.IdProject, invitationsToInsert);
                }
            }
            else
            {
                throw new HttpResponseException("No data is found");

            }

            if(invitationsToInsert.Count > 0)
            {
                await _invitationRepository.CreateManyInvitations(invitationsToInsert);
            }
            return newSupervisors;
        }

        public async Task<ProjectPromoter> InsertProjectPromoter(Invitation invitation)
        {
            var newProjectPromoter = new ProjectPromoter
            {
                Project = invitation.Project,
                User = invitation.For_Who
            };
            var insertedPromoter = await _projectPromotersRepository.CreateProjectPromoter(newProjectPromoter);
            return insertedPromoter;
        }

        public async Task<Invitation> searchSupervisorUserAndCreateInvitation(String userEmail, int index, int idProject, List<Invitation> invitations)
        {
            var newInvitation = new Invitation();

            var supervisor = await _projectPromotersRepository.GetProjectPromoterByEmail(userEmail);
            if (supervisor == null)
            {
                throw new HttpResponseException("Supervisor " + index + 
                                                " Email is not found. Please enter correct supervisor email");
            }
            else
            {
                var invitation = new Invitation
                {
                    Project = idProject,
                    For_Who = supervisor.IdUser,
                    IsMemberInvitation = false,
                    IsActive = true
                };
                
                invitations.Add(invitation);
            }

            return newInvitation;
        }
    }
}
