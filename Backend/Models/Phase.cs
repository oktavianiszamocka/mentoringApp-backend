using System;
using System.Collections.Generic;

namespace MentorApp.Models
{
    public partial class Phase
    {
        public int IdPhase { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int ProjectIdProject { get; set; }

        public virtual Project ProjectIdProjectNavigation { get; set; }
    }
}
