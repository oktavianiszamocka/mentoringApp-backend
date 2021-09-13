using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentorApp.DTOs.Requests
{
    public class ProjectMemberUpdateWrapper
    {
        public int IdProjectMember { get; set; }
        public int IdNewRole { get; set; }
    }
}
