using System;
using System.Collections.Generic;

namespace MentorApp.Models
{
    public partial class Note
    {
        public int IdNote { get; set; }
        public string Title { get; set; }
        public string Subject { get; set; }
        public int Author { get; set; }
        public string Note1 { get; set; }
        public int Meeting { get; set; }
        public string Attachments { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? LastModified { get; set; }

        public virtual User AuthorNavigation { get; set; }
        public virtual Meeting MeetingNavigation { get; set; }
    }
}
