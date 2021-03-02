using MentorApp.Models;
using System.Threading.Tasks;

namespace MentorApp.Repository
{
    public interface IProfileRepository
    {
        Task<Profile> GetUserProfile(int IdUser);
        Task<Profile> UpdateUserProfile(Profile Profile);
    }
}
