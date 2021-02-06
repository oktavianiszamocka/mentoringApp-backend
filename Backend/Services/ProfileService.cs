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
        private readonly IProjectRepository _profileRepository;
        public ProfileService(IProjectRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }
        public async Task<Profile> GetUserProfile(int IdUser)
        {
            return await _profileRepository.GetUserProfile(IdUser);
        }
    }
}
