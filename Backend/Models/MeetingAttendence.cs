

namespace MentorApp.Models
{
    public partial class MeetingAttendence
    {
        public int IdAttendence { get; set; }
        public int Meeting { get; set; }
        public int? User { get; set; }
        public bool? IsAttend { get; set; }

        public virtual Meeting MeetingNavigation { get; set; }
        public virtual User UserNavigation { get; set; }
    }
}
