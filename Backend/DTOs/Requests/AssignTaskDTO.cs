using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentorApp.DTOs.Requests
{
    public class AssignTaskDTO
    {
        public String userName { get; set; }
        public String email { get; set; }
        public String taskName { get; set; }
    }
}
