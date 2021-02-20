using AutoMapper;
using MentorApp.DTOs.Requests;
using MentorApp.DTOs.Responses;
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
        private readonly IMapper _mapper;
        public ProfileService(IProfileRepository profileRepository, IUserRepository userRepository, IMapper mapper)
        {
            _profileRepository = profileRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<ProfileDTO> GetUserProfile(int IdUser)
        {
            var userProfile = await _profileRepository.GetUserProfile(IdUser);
            var profileDTO = _mapper.Map<ProfileDTO>(userProfile);

            profileDTO.FirstName = userProfile.UserNavigation.FirstName;
            profileDTO.LastName = userProfile.UserNavigation.LastName;
            profileDTO.Email = userProfile.UserNavigation.Email;
            profileDTO.Avatar = userProfile.UserNavigation.Avatar;

            return profileDTO;
        }

        public async Task<Models.Profile> UpdateUserProfile(EditProfileDTO ProfileDTO)
        {
            var profile = new Models.Profile
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
