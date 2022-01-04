using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentorApp.DTOs.Requests
{
    public class MeetingRequestDto
    {
        public int IdMeeting { get; set; }
        public string Title { get; set; }
        public DateTime? MeetingDate { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public int Project { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public List<int> AttendeeUsers { get; set; }

        public Boolean IsAddNewAttendee { get; set; }
        public Boolean IsRemoveAttendee { get; set; }
        public List<int> AttendeeToRemove { get; set; }
        public List<int> AttendeeToAdd { get; set; }


    }
}
