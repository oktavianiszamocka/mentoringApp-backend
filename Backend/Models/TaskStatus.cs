
using System.Collections.Generic;

namespace MentorApp.Models
{
    public partial class TaskStatus
    {
        public TaskStatus()
        {
            Task = new HashSet<Task>();
        }

        public int IdStatus { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Task> Task { get; set; }
    }
}
