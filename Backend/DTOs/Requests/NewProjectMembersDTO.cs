using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentorApp.DTOs.Requests
{
    public class NewProjectMembersDTO
    {
        public int IdProject { get; set; }
        public List<NewMember> NewMembers { get; set; }
    }

    public class NewMember{
        public String MemberEmail { get; set; }
        public int Role { get; set; }
    }
}
