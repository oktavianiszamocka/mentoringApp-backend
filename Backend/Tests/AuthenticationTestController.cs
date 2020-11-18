using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MentorApp.Models;
using MentorApp.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MentorApp.Tests
{
    [Route("api/test")]
    public class AuthenticationTestController : Controller
    {
/*        private readonly MentorAppContext _context;
        private readonly UserManager<AppUser> _userManager;*/

        [HttpGet("public-data")]
        public string PublicTest()
        {
            return "This data is public";
        }

        [Authorize]
        [HttpGet("secret-data")]
        public string SecretData()
        {
            return "This data is secret";
        }

        [Authorize(Roles = "admin")]
        [HttpGet("secret-data-for-admin")]
        public string SecretDataForAdmin()
        {
            return "This data is secret and only for admins";
        }

        //check
        [Authorize(Roles = "admin2")]
        [HttpGet("secret-data-for-admin2")]
        public string SecretDataForAdmin2()
        {
            return "This data is secret and only for admins2";
        }
    }
}