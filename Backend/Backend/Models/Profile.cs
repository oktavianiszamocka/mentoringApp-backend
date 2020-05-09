using System;
using System.Collections.Generic;

namespace Backend.Models
{
    public partial class Profile
    {
        public int IdProfile { get; set; }
        public string Major { get; set; }
        public string Skills { get; set; }
        public string Experiences { get; set; }
        public int? Semester { get; set; }
        public int User { get; set; }

        public virtual User UserNavigation { get; set; }
    }
}
