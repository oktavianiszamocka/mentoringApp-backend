using System.Threading.Tasks;
using MentorApp.DTOs.Requests;
using MentorApp.DTOs.Responses;
using MentorApp.Services;
using MentorApp.Wrappers;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        [HttpGet("{idUser:int}")]
        public async Task<IActionResult> GetUserProfile(int idUser)
        {
            var profileUser = await _profileService.GetUserProfile(idUser);
            return Ok(new Response<ProfileDTO>(profileUser));
        }

        [Authorize]
        [HttpGet("user/{idUser:int}")]
        public async Task<IActionResult> GetUserImageAndName(int idUser)
        {
            var userWrapper = await _userService.GetUserById(idUser);
            return Ok(new Response<UserWrapper>(userWrapper));
        }

        [Authorize]
        [HttpPatch]
        public async Task<IActionResult> UpdateUserProfile(EditProfileDTO profileDTO)
        {
            await _profileService.UpdateUserProfile(profileDTO);
            return StatusCode(200, profileDTO);
        }
    }
}