using System.Collections.Generic;

namespace MentorApp.DTOs.Requests
{
    public class EditPostDTO
    {
        public int IdPost { get; set; }

        public string Title { get; set; }
        public string Content { get; set; }
        public List<string> Tags { get; set; }
    }
}
