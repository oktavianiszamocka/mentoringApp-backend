using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentorApp.Models
{
    public class ProjectStudies
    {
        public ProjectStudies()
        {
            Project = new HashSet<Project>();
        }

        public int IdProjectStudies { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Project> Project { get; set; }
    }
}
