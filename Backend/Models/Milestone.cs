using System;

namespace MentorApp.Models
{
    public partial class Milestone
    {
        public int IdMilestone { get; set; }
        
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int Project { get; set; }
        public int Sequence { get; set; }

        public virtual Project ProjectNavigation { get; set; }
    }
}
