

namespace MentorApp.Models
{
    public partial class TaskAssigning
    {
        public int IdAssign { get; set; }
        public int Task { get; set; }
        public int User { get; set; }

        public virtual Task TaskNavigation { get; set; }
        public virtual User UserNavigation { get; set; }
    }
}
