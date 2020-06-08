using System;
using System.Collections.Generic;

namespace MentorApp.Models
{
    public partial class ProjectStatus
    {
        public ProjectStatus()
        {
            Project = new HashSet<Project>();
        }

        public int IdStatus { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Project> Project { get; set; }
    }
}
