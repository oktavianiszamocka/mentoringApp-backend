using MentorApp.Models;
using System.Threading.Tasks;

namespace MentorApp.Repository
{
    public interface IProjectPromotersRepository
    {
        Task<Project> GetProjectPromoters(int IdProject);

        Task<User> GetProjectPromoterByEmail(string emailUser);
    }
}
