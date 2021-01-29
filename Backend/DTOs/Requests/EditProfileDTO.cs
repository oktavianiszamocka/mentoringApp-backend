using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentorApp.DTOs.Requests
{
    public class EditProfileDTO
    {
        public int IdUser { get; set; }
        public int IdProfile { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Country { get; set; }
        public string Major { get; set; }
        public int? Semester { get; set; }
        public string Skills { get; set; }
        public string Experiences { get; set; }

    }
}
