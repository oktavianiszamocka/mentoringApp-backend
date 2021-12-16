using System;
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
        Task<User> GetUserByEmail(string email);
        Task<User> CreateNewUser(User newUser, Profile newProfile);
        Task<User> UpdateUserAvatar(int idUser, String pictureUrl);
        Task<User> ChangeUserPassword(int idUser, string oldPassword, string newPassword);
        Task<User> SavePasswordResetToken(string token, string userEmail);

    }
}
