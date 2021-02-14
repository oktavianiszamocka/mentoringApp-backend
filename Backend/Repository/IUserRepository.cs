using MentorApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentorApp.Repository
{
    public interface IUserRepository
    {
        Task<User> GetUserById(int IdUser);
        Task<User> UpdateProfileUser(User User);
    }
}
