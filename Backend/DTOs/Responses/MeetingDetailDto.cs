using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MentorApp.Wrappers;

namespace MentorApp.DTOs.Responses
{
    public class MeetingDetailDto
    {
        public int IdMeeting { get; set; }
        public string Title { get; set; }
        public DateTime? MeetingDate { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public int? Project { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public List<UserAttendeeWrapper> MeetingAttendee { get; set; }
    }

    public class UserAttendeeWrapper
    {
        public int IdUser { get; set; }
        public int IdAttendence { get; set; }
        public String firstName { get; set; }
        public String lastName { get; set; }
        public String imageUrl { get; set; }
        public bool? isAttend { get; set; }
    }
}
