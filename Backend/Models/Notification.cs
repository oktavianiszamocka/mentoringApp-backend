using System;

namespace MentorApp.Models
{
    public partial class Notification
    {
        public int IdNotification { get; set; }
        public string Notification1 { get; set; }
        public int User { get; set; }
        public DateTime CreatedOn { get; set; }

        public virtual User UserNavigation { get; set; }
    }
}
