using System.Collections.Generic;
using MentorApp.Models;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

namespace MentorApp.Repository
{
    public interface IProjectPromotersRepository
    {
        Task<Project> GetProjectPromoters(int IdProject);
        Task<List<Project>> GetPromotorProjects(int idUser);

        Task<User> GetProjectPromoterByEmail(string emailUser);

        Task<ProjectPromoter> GetProjectPromoterByIdProjectAndIdUser(int idProject, int idUser);
        Task<ProjectPromoter> CreateProjectPromoter(ProjectPromoter promoter);
        Task<ProjectPromoter> DeleteProjectPromoter(int idProjectPromoter);
        Task<List<ProjectPromoter>> GetAdditionalPromoters(int idProject);

    }
}
