using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
