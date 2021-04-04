using MentorApp.DTOs.Responses;
using System.Threading.Tasks;
using MentorApp.DTOs.Requests;
using MentorApp.Models;
using Microsoft.AspNetCore.Server.Kestrel;


namespace MentorApp.Services
{
    public interface IProjectPromoterService
    {

        Task<ProjectPromotersDTO> GetProjectPromoters(int IdProject);

        Task<NewSupervisorsProjectDTO> CreateProjectPromoter(NewSupervisorsProjectDTO newSupervisors);
        Task<ProjectPromoter> InsertProjectPromoter(Invitation invitation);
    }
}
