using System;
using System.Collections.Generic;

namespace MentorApp.Models
{
    public partial class ProjectPromoter
    {
        public int IdProjectPromoter { get; set; }
        public int Project { get; set; }
        public int User { get; set; }

        public virtual Project ProjectNavigation { get; set; }
        public virtual User UserNavigation { get; set; }
    }
}
