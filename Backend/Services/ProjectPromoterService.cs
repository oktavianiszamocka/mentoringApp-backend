using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using MentorApp.DTOs.Responses;
using MentorApp.Repository;
using MentorApp.Wrappers;
using System.Linq;
using System.Threading.Tasks;
using Amazon.S3.Model;
using MentorApp.DTOs.Requests;
using MentorApp.Helpers;
using MentorApp.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic.CompilerServices;

namespace MentorApp.Services
{
    public class ProjectPromoterService : IProjectPromoterService
    {

        private readonly IProjectPromotersRepository _projectPromotersRepository;
        private readonly IInvitationRepository _invitationRepository;
        private static int MAX_PROMOTOR_NUMBER = 4;

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
            var uniqueNewSupervisorEnurable =  newSupervisors.SupervisorEmails.Distinct();
            var uniqueNewSupervisor = uniqueNewSupervisorEnurable.ToList();
            var invitationsPromotorProject = await _invitationRepository.GetInvitationProjectPromoterByProject(newSupervisors.IdProject);
            var invitationsPromotorProjectEmails =
                invitationsPromotorProject.Select(inv => inv.UserNavigation.Email).ToList();

            if (newSupervisors.SupervisorEmails.Count > 0)
            {
                var projectPromotorProject =
                    await _projectPromotersRepository.GetAdditionalPromoters(newSupervisors.IdProject);


                for (int i = 0; i < uniqueNewSupervisor.Count(); i++)
                {
                    if (uniqueNewSupervisor[i] != "")
                    {
                        await searchSupervisorUserAndCreateInvitation(uniqueNewSupervisor[i], i + 2,  newSupervisors.IdProject,
                            projectPromotorProject.Count, invitationsPromotorProjectEmails.Count,
                             invitationsPromotorProjectEmails, invitationsToInsert);
                    }
                }
            }
            else
            {
                throw new HttpResponseException("No data is found");
            }

            if(invitationsToInsert.Count > 0)
            { 
                if ((invitationsPromotorProjectEmails.Count + invitationsToInsert.Count) > MAX_PROMOTOR_NUMBER)
                {
                    
                    throw new HttpResponseException(
                            "Sum of Number of Project Promotor and pending invitation of this project is already maximum (4) ! To add new project promotor please wait for invitation approval!");
                    
                }
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

        public async Task<List<string>> GetAdditionalPromoterEmails(int idProject)
        {
            var additionalPromoters = await _projectPromotersRepository.GetAdditionalPromoters(idProject);
            var additionalPromoterEmails = additionalPromoters
                .Select(promoter => promoter.UserNavigation.Email).ToList();
            return additionalPromoterEmails;
        }

        public async Task<ProjectPromoter> DeleteProjectPromoter(int idProject, string emailUser)
        {
            var userId = await _projectPromotersRepository.GetProjectPromoterByEmail(emailUser);
            if (userId == null)
            {
                throw new HttpResponseException(emailUser + " is not Found in database");
            }
            var projectPromoterUser =
                await _projectPromotersRepository.GetProjectPromoterByIdProjectAndIdUser(idProject, userId.IdUser);
            await _projectPromotersRepository.DeleteProjectPromoter(projectPromoterUser.IdProjectPromoter);
            return projectPromoterUser;
        }

        public async Task<Invitation> searchSupervisorUserAndCreateInvitation(
            String userEmail,
            int index,
            int idProject, 
            int countProjectMember, 
            int countInvitation, 
            List<String> existingInvitationEmails, 
            List<Invitation> invitations)
        {
            var newInvitation = new Invitation();

            var supervisor = await _projectPromotersRepository.GetProjectPromoterByEmail(userEmail);
            //if email not found
            if (supervisor == null)
            {
                throw new HttpResponseException(
                    "Email " + userEmail + " is not found. Please enter correct supervisor email");
            }

            //if email found

            //if no project member or invitation yet
            if (countProjectMember == 0 && countInvitation == 0)
            {
                var invitation = new Invitation
                {
                    Project = idProject,
                    For_Who = supervisor.IdUser,
                    IsMemberInvitation = false,
                    IsActive = true
                };

                invitations.Add(invitation);
                return invitation;
            }


            if (countProjectMember > 0 || countInvitation > 0)
            {
                if (existingInvitationEmails.Contains(userEmail))
                {
                    throw new HttpResponseException(
                        "Invitation for " + userEmail +
                        " has been sent already. Please wait for his approval!");
                }
               
                if ((countProjectMember + countInvitation ) > MAX_PROMOTOR_NUMBER)
                {
                    throw new HttpResponseException(
                        "Sum of Number of Project Promotor and pending invitation of this project is already maximum (4) ! To add new project promotor please wait for invitation approval!");
                }

                var projectPromoter =
                    await _projectPromotersRepository.GetProjectPromoterByIdProjectAndIdUser(idProject, supervisor.IdUser);
                if (projectPromoter == null)
                {
                    var invitation = new Invitation
                    {
                        Project = idProject,
                        For_Who = supervisor.IdUser,
                        IsMemberInvitation = false,
                        IsActive = true
                    };

                    invitations.Add(invitation);
                    return invitation;
                }
            }

            return newInvitation;
        
        }
    }
}
