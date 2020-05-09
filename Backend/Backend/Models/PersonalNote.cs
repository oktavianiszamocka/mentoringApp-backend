using System;
using System.Collections.Generic;

namespace Backend.Models
{
    public partial class PersonalNote
    {
        public int IdNote { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int User { get; set; }

        public virtual User UserNavigation { get; set; }
    }
}
