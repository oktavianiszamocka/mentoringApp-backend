using System;

namespace MentorApp.Models
{
    public partial class ProjectHistory
    {
        public int IdHistory { get; set; }
        public DateTime Date { get; set; }
        public string Change { get; set; }
        public int Project { get; set; }
        public int WhoChange { get; set; }

        public virtual Project ProjectNavigation { get; set; }
        public virtual User WhoChangeNavigation { get; set; }
    }
}
