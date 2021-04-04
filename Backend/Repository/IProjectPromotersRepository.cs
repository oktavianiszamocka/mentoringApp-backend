using MentorApp.Models;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

namespace MentorApp.Repository
{
    public interface IProjectPromotersRepository
    {
        Task<Project> GetProjectPromoters(int IdProject);

        Task<User> GetProjectPromoterByEmail(string emailUser);

        Task<ProjectPromoter> CreateProjectPromoter(ProjectPromoter promoter);
    }
}
