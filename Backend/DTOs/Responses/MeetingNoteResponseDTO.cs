using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentorApp.DTOs.Responses
{
    public class MeetingNoteResponseDTO
    {
        public int IdNote { get; set; }
        public string Title { get; set; }
        public string Subject { get; set; }
        public int Author { get; set; }
        public string AuthorFirstName { get; set; }
        public string AuthorLastName { get; set; }
        public string Note1 { get; set; }
        public int Meeting { get; set; }
        public string Attachments { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastModified { get; set; }
    }
}
