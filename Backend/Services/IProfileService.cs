using MentorApp.DTOs.Requests;
using MentorApp.DTOs.Responses;
using MentorApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentorApp.Services
{
    public interface IProfileService
    {
        Task<ProfileDTO> GetUserProfile(int IdUser);
        Task<Profile> UpdateUserProfile(EditProfileDTO ProfileDTO);
    }
}
