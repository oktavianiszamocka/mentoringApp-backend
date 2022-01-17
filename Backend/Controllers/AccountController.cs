using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using MentorApp.DTOs.Requests;
using MentorApp.Helpers;
using MentorApp.Persistence;
using MentorApp.Services;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IMailService _mailService;

        public AccountController(MentorAppContext context, IConfiguration configuration, IUserService userService, IMailService mailService)
        {
            _context = context;
            _configuration = configuration;
            _userService = userService;
            _mailService = mailService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginRequestDTO request)
        {
            try
            {
                var user = await _userService.Authenticate(request);

                if (user == null)
                    return BadRequest(new { message = "Username or password is incorrect" });

/*                Claim[] userclaim =
                {
                    //new Claim(ClaimTypes.Name, user.FirstName),
                    new Claim(ClaimTypes.Role, "mentor")
                    //Add additional data here
                };*/


                Claim[] userclaim = new Claim[1];
                if(user.Role == 3)
                {
                    userclaim[0] = new Claim(ClaimTypes.Role, "mentor");
                } else if(user.Role == 1)
                {
                    userclaim[0] = new Claim(ClaimTypes.Role, "admin");
                } else
                {
                    userclaim[0] = new Claim(ClaimTypes.Role, "student");
                }

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
                _context.User.Update(user);
                await _context.SaveChangesAsync();

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    refreshToken = user.RefreshToken,
                    idUser = user.IdUser,
                    role = user.Role
                });

            }
            catch(HttpResponseException exception)
            {
                return StatusCode(500, exception.Value);
            }
           
        }

        [HttpPost("{refreshToken}/refresh")]
        public async Task<IActionResult> RefreshToken([FromRoute] string refreshToken)
        {
            var user = _context.User.SingleOrDefault(m => m.RefreshToken == refreshToken);
            if(user == null) return NotFound("Refresh token not found");

            //TODO Here we should additionally check if the refresh token hasn't expired!
            if(user.RefreshTokenExpDate < DateTime.Now) return NotFound("Refresh token expired");

            Claim[] userclaim = new Claim[1];
            if (user.Role == 3)
            {
                userclaim[0] = new Claim(ClaimTypes.Role, "mentor");
            }
            else if (user.Role == 1)
            {
                userclaim[0] = new Claim(ClaimTypes.Role, "admin");
            }
            else
            {
                userclaim[0] = new Claim(ClaimTypes.Role, "student");
            }

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
            _context.User.Update(user);
            await _context.SaveChangesAsync();
            Console.WriteLine(DateTime.Now + user.RefreshToken);

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                refreshToken = user.RefreshToken
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationDTO request)
        {
            try
            {
                var authResponse = await _userService.Register(request);

                return Ok(authResponse);
            }
            catch (HttpResponseException exception)
            {
                return StatusCode(500, exception.Value);
            }
            
        }


        [Authorize]
        [HttpPatch("avatar")]
        public async Task<IActionResult> UpdateAvatar([FromQuery(Name = "user")] int idUser, [FromQuery(Name = "url")]  String pictureUrl)
        {
            var userTarget = await _userService.UpdateUserAvatar(idUser, pictureUrl);
            return StatusCode(200, userTarget);
        }

        [Authorize]
        [HttpPost("changePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] PasswordChangeDTO passowrdChangeDTO)
        {
            try
            {
                var response = await _userService.ChangePassword(passowrdChangeDTO);
                return Ok(response);
            } catch(HttpResponseException ex)
            {
                return StatusCode(500, ex.Value);
            }
        }


        [HttpPost("send")]
        public async Task<IActionResult> SendMail([FromForm]MailRequest request)
        {
            try
            {
                await _mailService.SendEmailAsync(request);
                return Ok();
            }
            catch(HttpResponseException ex)
            {
                return StatusCode(500, ex.Value);
            }

        }

        [HttpPost("welcome")]
        public async Task<IActionResult> SendWelcomeMail([FromForm]WelcomeRequest request)
        {
            try
            {
                await _mailService.SendWelcomeEmailAsync(request);
                return Ok();
            }
            catch(HttpResponseException ex)
            {
                return StatusCode(500, ex.Value);
            }
        }

        [HttpPost("sendReset")]
        public async Task<IActionResult> SendResetPassword([FromBody] ResetPasswordDTO resetData)
        {
            try
            {
                await _mailService.SendResetPasswordEmailAsync(resetData.email);
                return Ok();
            }
            catch(HttpResponseException ex)
            {
                return StatusCode(500, ex.Value);
            }
        }

        [HttpPost("resetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDTO resetData)
        {
            try
            {
                await _mailService.ResetPasswordWithTokenAsync(resetData.newPassword, resetData.resetToken);
                return Ok();
            }
            catch (HttpResponseException ex)
            {
                return StatusCode(500, ex.Value);
            }
        }

    }
}