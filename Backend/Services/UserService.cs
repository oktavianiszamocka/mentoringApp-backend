using MentorApp.DTOs.Requests;
using MentorApp.Helpers;
using MentorApp.Models;
using MentorApp.Repository;
using MentorApp.Security;
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

        public async Task<AuthenticationResult> Register(UserRegistrationDTO request)
        {
            var existingUser = await _userRepository.GetUserByEmail(request.Email);

            if(existingUser != null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "User with this email address already exists" }
                };
            }

            var passwordHasher = new PasswordHasher(new HashingOptions() { /*Iterations = 20000 */}) ;
            var hashedPassword = passwordHasher.Hash(request.Password);

            var newUser = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Role = request.Role,
                Email = request.Email,
                Password = hashedPassword,
                //TODO set the salt
                Salt = "qwerty"
            };
            var newProfile = new Profile
            {
                Country = request.Country,
                Major = request.Major,
                Semester = request.Semester,
                Phone = request.Phone,
                DateOfBirth = request.DateOfBirth,
                User = 0
            };

            var createdUser = await _userRepository.CreateNewUser(newUser, newProfile);

            if(createdUser == null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "Error in creating an user" }
                };
            }

            return new AuthenticationResult
            {
                Success = true
            };

        }


        public async Task<User> UpdateUserAvatar(int idUser, string pictureUrl)
        {
            return await _userRepository.UpdateUserAvatar(idUser, pictureUrl);
            
        }
            

        public async Task<User> ChangePassword(PasswordChangeDTO passwordChangeDTO)
        {
            var user = await _userRepository.ChangeUserPassword(passwordChangeDTO.idUser, passwordChangeDTO.oldPassword, passwordChangeDTO.newPassword);
            if(user == null)
            {
                throw new HttpResponseException("Incorrect old password, try again");
            }
            return await _userRepository.ChangeUserPassword(passwordChangeDTO.idUser, passwordChangeDTO.oldPassword, passwordChangeDTO.newPassword);
        }
    }
}
