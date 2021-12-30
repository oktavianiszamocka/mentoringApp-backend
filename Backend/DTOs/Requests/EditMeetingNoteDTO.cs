using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentorApp.DTOs.Requests
{
    public class EditMeetingNoteDTO
    {

        public int IdNote { get; set; }

        public string Subject { get; set; }
        public DateTime? LastModified { get; set; }
        public string Note { get; set; }
    }
}
