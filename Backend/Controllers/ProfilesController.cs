using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MentorApp.DTOs.Requests;
using MentorApp.DTOs.Responses;
using MentorApp.Models;
using MentorApp.Services;
using MentorApp.Wrappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MentorApp.Controllers
{
    [Route("api/profiles")]
    [ApiController]
    public class ProfilesController : ControllerBase
    {
        private readonly IProfileService _profileService;
        private readonly IUserService _userService;

        public ProfilesController(IProfileService profileService, IUserService userService)
        {
            _profileService = profileService;
            _userService = userService;
        }

        [HttpGet("{IdUser:int}")]
        public async Task<IActionResult> GetUserProfile(int IdUser)
        {
            var profileUser = await _profileService.GetUserProfile(IdUser);
            return Ok(new Response<ProfileDTO>(profileUser));
        }

        [HttpGet("user/{IdUser:int}")]
        public async Task<IActionResult> GetUserImageAndName(int IdUser)
        {
            var userWrapper = await _userService.GetUserById(IdUser);
            return Ok(new Response<UserWrapper>(userWrapper));
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateUserProfile(EditProfileDTO ProfileDTO)
        {
            await _profileService.UpdateUserProfile(ProfileDTO);
            return StatusCode(200, ProfileDTO);
        }
    }
}