using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentorApp.DTOs.Requests
{
    public class NewSupervisorsProjectDTO
    {
        public int IdProject { get; set; }

        public List<String> SupervisorEmails { get; set; }

    }

}
