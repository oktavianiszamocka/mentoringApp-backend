using System;

namespace MentorApp.DTOs.Responses
{
    public class ProfileDTO
    {
        public int IdProfile { get; set; }
        public string Phone { get; set; }
        public string Country { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Major { get; set; }
        public string Skills { get; set; }
        public string Experiences { get; set; }
        public int? Semester { get; set; }
        public int User { get; set; }
        public string Email { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Avatar { get; set; }
    }
}
