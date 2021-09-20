﻿using MentorApp.DTOs.Requests;
using MentorApp.Models;
using MentorApp.Persistence;
using MentorApp.Security;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using MentorApp.Helpers;

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
            var user = await _context.User.FirstOrDefaultAsync(u => u.Email == loginRequest.Username);
            if (user == null)
            {
                throw new HttpResponseException("User does not exist! Create account before login");
            }
                

            var passwordHasher = new PasswordHasher(new HashingOptions() {});
            var passwordVerified = passwordHasher.Check(user.Password, loginRequest.Password);

            if (passwordVerified == false)
            {
                throw new HttpResponseException("Password is wrong. Please fill the correct password!");
            } else
            {
                return user;
            }

            /*var user = await _context.User.FirstOrDefaultAsync(u => u.Email == loginRequest.Username && u.Password == loginRequest.Password);
            if(user == null) 
                return null;*/
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _context.User
                        .Where(user => user.Email.Equals(email))
                        .FirstOrDefaultAsync();
        }

        public async Task<User> CreateNewUser(User newUser, Profile newProfile)
        {
            _context.User.Add(newUser);
            await _context.SaveChangesAsync();
            var newCreatedUser = await GetUserByEmail(newUser.Email);
            newProfile.User = newCreatedUser.IdUser;
            _context.Profile.Add(newProfile);
            await _context.SaveChangesAsync();
            return newCreatedUser;
        }

        public async Task<User> UpdateUserAvatar(int idUser, string pictureUrl)
        {
            var userProfile = await _context.User
                .Where(user => user.IdUser.Equals(idUser))
                .FirstOrDefaultAsync();

            userProfile.Avatar = pictureUrl;
            _context.User.Update(userProfile);
            await _context.SaveChangesAsync();
            return userProfile;
        }
    }
}
