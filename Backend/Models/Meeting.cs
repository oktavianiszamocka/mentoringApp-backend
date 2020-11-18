using System;
using System.Collections.Generic;

namespace MentorApp.Models
{
    public partial class Meeting
    {
        public Meeting()
        {
            MeetingAttendence = new HashSet<MeetingAttendence>();
            Note = new HashSet<Note>();
        }

        public int IdMeeting { get; set; }
        public string Title { get; set; }
        public DateTime DateTime { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public int? Project { get; set; }

        public virtual Project ProjectNavigation { get; set; }
        public virtual ICollection<MeetingAttendence> MeetingAttendence { get; set; }
        public virtual ICollection<Note> Note { get; set; }
    }
}
