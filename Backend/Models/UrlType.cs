using System.Collections.Generic;

namespace MentorApp.Models
{
    public partial class UrlType
    {

        public UrlType()
        {
            Urls = new HashSet<Url>();

        }
        public int IdUrlType { get; set; }
        public string UrlName { get; set; }

        public virtual ICollection<Url> Urls { get; set; }
    }
}
