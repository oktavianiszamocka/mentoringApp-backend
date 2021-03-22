using System;

namespace MentorApp.Models
{
    
        public partial class Invitation
        {
            public int IdInvitation{ get; set; }
            public int Project { get; set; }
            public int Role { get; set; }
            public int ForWho { get; set; }
            public Boolean IsMemberInvitation { get; set; }
            public Boolean IsAccepted { get; set; }

            public virtual User UserNavigation { get; set; }
            public virtual Project ProjectNavigation { get; set; }
            public virtual MemberRole MemberRoleNavigation { get; set; }
        }
    

}
