using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentorApp.DTOs.Requests
{
    public class EditPostDTO
    {
        public int IdPost { get; set; }

        public string Title { get; set; }
        public string Content { get; set; }
        public string Attachment { get; set; }
        public List<string> Tags { get; set; }
    }
}
