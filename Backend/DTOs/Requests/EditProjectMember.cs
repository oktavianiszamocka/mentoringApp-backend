using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentorApp.DTOs.Requests
{
    public class EditProjectMember
    {
        public int IdProject { get; set; }
        public List<int> RemoveProjectMember { get; set; }
        public List<ProjectMemberUpdateWrapper> ProjectMemberUpdateWrappers { get; set; }
    }

    public class ProjectMemberUpdateWrapper
    {
        public int IdProjectMember { get; set; }
        public int IdNewRole { get; set; }
    }
}
