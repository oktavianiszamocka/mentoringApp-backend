using System;

namespace MentorApp.Models
{
    public partial class PersonalNote
    {
        public int IdNote { get; set; }

        public string Description { get; set; }
        public int User { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? LastModified { get; set; }

        public virtual User UserNavigation { get; set; }
    }
}
