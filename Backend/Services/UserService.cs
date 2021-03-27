using MentorApp.DTOs.Requests;
using MentorApp.Models;
using MentorApp.Repository;
using MentorApp.Wrappers;
using System.Threading.Tasks;

namespace MentorApp.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<UserWrapper> GetUserById(int idUser)
        {
            var userInfo = await _userRepository.GetUserById(idUser);
            return GetUserWrapper(userInfo);
        }

        public async Task<User> Authenticate(LoginRequestDTO loginRequest)
        {
            return await _userRepository.Authenticate(loginRequest);
        }

        public UserWrapper GetUserWrapper(User user)
        {
            return new UserWrapper
            {
                IdUser = user.IdUser,
                firstName = user.FirstName,
                lastName = user.LastName,
                imageUrl = user.Avatar
            };
        }

        public async Task<User> UpdateProfileUser(User user)
        {
            return await _userRepository.UpdateProfileUser(user);
        }
    }
}
