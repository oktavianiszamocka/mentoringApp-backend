namespace MentorApp.Models
{
    public partial class Url
    {
        public int IdUrl { get; set; }
        public string Link { get; set; }
        public int Project { get; set; }
        public string Category { get; set; }

        public virtual Project ProjectNavigation { get; set; }
    }
}
