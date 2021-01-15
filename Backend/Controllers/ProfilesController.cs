﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            return Ok(new Response<Profile>(profileUser));
        }

        [HttpGet("user/{IdUser:int}")]
        public async Task<IActionResult> GetUserImageAndName(int IdUser)
        {
            var userWrapper = await _userService.GetUserById(IdUser);
            return Ok(new Response<UserWrapper>(userWrapper));
        }
    }
}