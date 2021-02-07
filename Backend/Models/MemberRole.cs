using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentorApp.Models
{
    public partial class MemberRole
    {
        public MemberRole()
        {
            ProjectMembers = new HashSet<ProjectMembers>();

        }
        public int IdRoleMember { get; set; }
        public string Role { get; set; }

        public virtual ICollection<ProjectMembers> ProjectMembers { get; set; }
    }
}
