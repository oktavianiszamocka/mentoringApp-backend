using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentorApp.DTOs.Requests
{
    public class NewMeetingNoteDTO
    {
        public int IdNote { get; set; }
        public string Title { get; set; }
        public string Subject { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string Author { get; set; }
        public string Note { get; set; }
        public int IdMeeting { get; set; }

    }
}

