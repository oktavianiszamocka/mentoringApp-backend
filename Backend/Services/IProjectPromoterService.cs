using System;
using System.Collections.Generic;
using MentorApp.DTOs.Responses;
using System.Threading.Tasks;
using MentorApp.DTOs.Requests;
using MentorApp.Models;
using Microsoft.AspNetCore.Server.Kestrel;
using Task = MentorApp.Models.Task;


namespace MentorApp.Services
{
    public interface IProjectPromoterService
    {

        Task<ProjectPromotersDTO> GetProjectPromoters(int IdProject);

        Task<NewSupervisorsProjectDTO> CreateProjectPromoter(NewSupervisorsProjectDTO newSupervisors);
        Task<ProjectPromoter> InsertProjectPromoter(Invitation invitation);

        Task<List<string>> GetAdditionalPromoterEmails(int idProject);

       // Task<EditProjectPromotersDTO> UpdateProjectPromoter(EditProjectPromotersDTO editProjectPromotersDto);
       Task<ProjectPromoter> DeleteProjectPromoter(int idProject, string emailUser);




    }
}
