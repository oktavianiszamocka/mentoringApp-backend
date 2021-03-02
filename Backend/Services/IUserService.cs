using MentorApp.Models;
using MentorApp.Wrappers;
using System.Threading.Tasks;

namespace MentorApp.Services
{
    public interface IUserService
    {
        Task<UserWrapper> GetUserById(int IdUser);
        Task<User> UpdateProfileUser(User User);
    }
}
