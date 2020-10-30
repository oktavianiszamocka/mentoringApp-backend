using System;
using System.Collections.Generic;

namespace MentorApp.Models
{
    public partial class Tag
    {
        public Tag()
        {
            PostTag = new HashSet<PostTag>();
        }

        public int IdTag { get; set; }
        public string Name { get; set; }

        public virtual ICollection<PostTag> PostTag { get; set; }
    }
}
