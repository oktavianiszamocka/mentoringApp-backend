using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentorApp.Models
{
    public class ProjectMode
    {
        public ProjectMode()
        {
            Project = new HashSet<Project>();
        }

        public int IdProjectMode { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Project> Project { get; set; }
    }
}
