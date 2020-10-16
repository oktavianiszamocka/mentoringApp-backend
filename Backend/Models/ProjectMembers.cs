namespace MentorApp.Models
{
    public partial class ProjectMembers
    {
        public int IdProjectMember { get; set; }
        public int Project { get; set; }
        public string Role { get; set; }
        public int Member { get; set; }

        public virtual User MemberNavigation { get; set; }
        public virtual Project ProjectNavigation { get; set; }
    }
}
