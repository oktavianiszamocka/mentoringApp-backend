using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentorApp.DTOs.Requests
{
    public class PasswordChangeDTO
    {
        public int userId;
        public string oldPassword;
        public string newPassword;
    }
}
