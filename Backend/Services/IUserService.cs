using MentorApp.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentorApp.Services
{
    public interface IUserService
    {
        Task<UserWrapper> GetUserById(int IdUser);
    }
}
