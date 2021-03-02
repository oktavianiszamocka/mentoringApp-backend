using System.Collections.Generic;

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
