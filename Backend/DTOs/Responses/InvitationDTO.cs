using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentorApp.DTOs.Responses
{
    public class InvitationDTO
    {
        public int IdInvitation { get; set; }
        public int IdProject { get; set; }
        public int For_Who { get; set; }
        public String ProjectName { get; set; }
        public String  Avatar { get; set; }
        public String ProjectOwnerName { get; set; }
        public int? Role { get; set; }
        public String RoleName { get; set; }
        public Boolean IsActive { get; set; }
        public Boolean IsMemberInvitation { get; set; }
        public Boolean IsAccepted { get; set; }



    }
}
