using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentorApp.DTOs.Responses
{
    public class InvitationProjectDTO
    {
        public int IdInvitation { get; set; }
        public int IdProject { get; set; }
        public String NameUser { get; set; }
        public int? Role { get; set; }
        public String RoleName { get; set; }
    }
}
