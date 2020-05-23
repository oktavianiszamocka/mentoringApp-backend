using System;
using System.Collections.Generic;

namespace Backend.Models
{
    public partial class Post
    {
        public Post()
        {
            Comment = new HashSet<Comment>();
        }

        public int IdPost { get; set; }
        public string Content { get; set; }
        public DateTime DateOfPublication { get; set; }
        public int Writer { get; set; }
        public int? Project { get; set; }
        public string Attachment { get; set; }

        public virtual Project ProjectNavigation { get; set; }
        public virtual User WriterNavigation { get; set; }
        public virtual ICollection<Comment> Comment { get; set; }
    }
}
