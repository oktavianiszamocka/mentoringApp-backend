using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentorApp.DTOs.Requests
{
    public class ResetPasswordDTO
    {
        public string email { get; set; }
        public string resetToken { get; set; }
        public string newPassword { get; set; }
    }

}
