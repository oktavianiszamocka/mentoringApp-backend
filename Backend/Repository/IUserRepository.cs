using MentorApp.DTOs.Requests;
using MentorApp.Models;
using System.Threading.Tasks;

namespace MentorApp.Repository
{
    public interface IUserRepository
    {
        Task<User> GetUserById(int IdUser);
        Task<User> UpdateProfileUser(User User);
        Task<User> Authenticate(LoginRequestDTO loginRequest);
    }
}
