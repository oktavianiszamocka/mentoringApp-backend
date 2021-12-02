using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Roles = "student")]
        [HttpGet("secret-data-for-student")]
        public string SecretDataForAdmin()
        {
            return "This data is secret and only for students";
        }

        //check
        [Authorize(Roles = "mentor")]
        [HttpGet("secret-data-for-mentor")]
        public string SecretDataForAdmin2()
        {
            return "This data is secret and only for mentor";
        }
    }
}