using System;
using System.Collections.Generic;

namespace Backend.Models
{
    public partial class Milestone
    {
        public int IdMilestone { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int Project { get; set; }

        public virtual Project ProjectNavigation { get; set; }
    }
}
