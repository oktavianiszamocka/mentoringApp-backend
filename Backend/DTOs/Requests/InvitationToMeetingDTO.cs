using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentorApp.DTOs.Requests
{
    public class InvitationToMeetingDTO
    {
        public String userName { get; set; }
        public String email { get; set; }
        public String meetingName { get; set; }
    }
}
