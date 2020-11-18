
namespace MentorApp.Models
{
    public partial class PostTag
    {
        public int IdPostTag { get; set; }
        public int Tag { get; set; }
        public int Post { get; set; }

        public virtual Post PostNavigation { get; set; }
        public virtual Tag TagNavigation { get; set; }
    }
}
