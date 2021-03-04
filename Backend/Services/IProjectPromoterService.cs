using MentorApp.DTOs.Responses;
using System.Threading.Tasks;


namespace MentorApp.Services
{
    public interface IProjectPromoterService
    {

        Task<ProjectPromotersDTO> GetProjectPromoters(int IdProject);
    }
}
