using MentorApp.DTOs.Requests;
using MentorApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentorApp.Services
{
    public interface IProfileService
    {
        Task<Profile> GetUserProfile(int IdUser);
        Task<Profile> UpdateUserProfile(EditProfileDTO ProfileDTO);
    }
}
