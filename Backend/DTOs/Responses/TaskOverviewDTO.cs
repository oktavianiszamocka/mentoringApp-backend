using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentorApp.DTOs.Responses
{
    public class TaskOverviewDTO
    {
        public int Status{ get; set; }

        public List<TaskDTO> Tasks { get; set; }          
        

    }

    public class TaskDTO
    {
        public int IdTask { get; set; }
        public string Title { get; set; }
        public string Priority { get; set; }
        public int StatusCode { get; set; }
        public String Status { get; set; }
        public DateTime? ExpectedEndDate { get; set; }
        public List<String> AssignedUserAvatars { get; set; }
    }
}
