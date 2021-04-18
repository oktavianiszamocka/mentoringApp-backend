using System.ComponentModel.DataAnnotations;

namespace MentorApp.DTOs.Requests
{
    public class LoginRequestDTO
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
