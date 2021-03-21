using MentorApp.DTOs.Responses;
using MentorApp.Models;
using MentorApp.Wrappers;
using System.Threading.Tasks;

namespace MentorApp.Services
{
    public interface IUserService
    {
        Task<UserWrapper> GetUserById(int idUser);
        Task<User> UpdateProfileUser(User user);
        Task<User> Authenticate(LoginRequestDTO loginRequest);
    }
}
