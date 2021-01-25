using MentorApp.Models;
using MentorApp.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentorApp.Repository
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly MentorAppContext _context;
        public ProfileRepository(MentorAppContext context)
        {
            _context = context;
        }
        public async Task<Profile> GetUserProfile(int IdUser)
        {
            return await _context.Profile
                        .Where(profile => profile.User.Equals(IdUser))
                        .FirstOrDefaultAsync();
        }

        public async Task<Profile> UpdateUserProfile(Profile Profile)
        {
            var existingProfile = await _context.Profile.FindAsync(Profile.IdProfile);
            existingProfile.Phone = Profile.Phone;
            existingProfile.Major = Profile.Major;
            existingProfile.Skills = Profile.Skills;
            existingProfile.Experiences = Profile.Experiences;
            existingProfile.Semester = Profile.Semester;
            _context.Profile.Update(existingProfile);
            await _context.SaveChangesAsync();
            return Profile;
        }


    }
}
