using MentorApp.Models;
using MentorApp.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentorApp.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly MentorAppContext _context;

        public UserRepository(MentorAppContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserById(int IdUser)
        {
            return await _context.User
                        .Where(user => user.IdUser.Equals(IdUser))
                        .FirstOrDefaultAsync();
        }

        public async Task<User> UpdateProfileUser(User User)
        {
            var existingUser = await _context.User.FindAsync(User.IdUser);
            existingUser.FirstName = User.FirstName;
            existingUser.LastName = User.LastName;
            existingUser.Email = User.Email;
            _context.User.Update(existingUser);
            await _context.SaveChangesAsync();
            return User;
        }
    }
}
