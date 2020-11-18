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


    }
}
