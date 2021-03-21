using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using MentorApp.DTOs.Responses;
using MentorApp.Persistence;
using MentorApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace MentorApp.Controllers
{
    [Route("api/account")]
    public class AccountController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly MentorAppContext _context;
        private readonly IUserService _userService;

        public AccountController(MentorAppContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDTO request)
        {
            //TODO Here we should check the credentials! Here we are just taking the first user.
            //var user = _context.User.ToList().First();
            var user = await _userService.Authenticate(request);

            if (user == null) return NotFound();

            Claim[] userclaim =
            {
                new Claim(ClaimTypes.Name, user.FirstName),
                new Claim(ClaimTypes.Role, "user"),
                new Claim(ClaimTypes.Role, "admin")
                //Add additional data here
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                "https://localhost:5001",
                "https://localhost:5001",
                userclaim,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: creds
            );


            user.RefreshToken = Guid.NewGuid().ToString();
            user.RefreshTokenExpDate = DateTime.Now.AddDays(1);
            _context.SaveChanges();

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                refreshToken = user.RefreshToken
            });
        }

        [HttpPost("{refreshToken}/refresh")]
        public IActionResult RefreshToken([FromRoute] string refreshToken)
        {
            var user = _context.User.SingleOrDefault(m => m.RefreshToken == refreshToken);
            if (user == null) return NotFound("Refresh token not found");

            //TODO Here we should additionally check if the refresh token hasn't expired!

            Claim[] userclaim =
            {
                new Claim(ClaimTypes.Name, user.FirstName),
                new Claim(ClaimTypes.Role, "user"),
                new Claim(ClaimTypes.Role, "admin")
                //Add additional data here
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                "https://localhost:5001",
                "https://localhost:5001",
                userclaim,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: creds
            );

            user.RefreshToken = Guid.NewGuid().ToString();
            user.RefreshTokenExpDate = DateTime.Now.AddDays(1);
            _context.SaveChanges();

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                refreshToken = user.RefreshToken
            });
        }
    }
}