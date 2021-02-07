using MentorApp.DTOs.Requests;
using MentorApp.Models;
using MentorApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentorApp.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IProfileRepository _profileRepository;
        private readonly IUserRepository _userRepository;
        public ProfileService(IProfileRepository profileRepository, IUserRepository userRepository)
        {
            _profileRepository = profileRepository;
            _userRepository = userRepository;
        }
        public async Task<Profile> GetUserProfile(int IdUser)
        {
            return await _profileRepository.GetUserProfile(IdUser);
        }

        public async Task<Profile> UpdateUserProfile(EditProfileDTO ProfileDTO)
        {
            var profile = new Profile
            {
                IdProfile = ProfileDTO.IdProfile,
                Phone = ProfileDTO.Phone,
                Country = ProfileDTO.Country,
                DateOfBirth = ProfileDTO.DateOfBirth,
                Major = ProfileDTO.Major,
                Skills = ProfileDTO.Skills,
                Experiences = ProfileDTO.Experiences,
                Semester = ProfileDTO.Semester
            };

            profile =  await _profileRepository.UpdateUserProfile(profile);

            var user = new User
            {
                IdUser = ProfileDTO.IdUser,
                FirstName = ProfileDTO.FirstName,
                LastName = ProfileDTO.LastName,
                Email = ProfileDTO.Email
            };

            user = await _userRepository.UpdateProfileUser(user);

            return profile;
        }
    }
}
