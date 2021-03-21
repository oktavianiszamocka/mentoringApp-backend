using MentorApp.DTOs.Responses;
using MentorApp.Models;
using MentorApp.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
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

        public async Task<User> Authenticate(LoginRequestDTO loginRequest)
        {
            /*            var user = await _context.User
                                    .Where(x => x.Email.Equals(loginRequest.Username) && x.Password.Equals(loginRequest.Password))
                                    .FirstOrDefaultAsync();*/
            var user = await _context.User.FirstOrDefaultAsync(u => u.Email == loginRequest.Username && u.Password == loginRequest.Password);
            if(user == null) 
                return null;
            return user;
        }
    }
}
