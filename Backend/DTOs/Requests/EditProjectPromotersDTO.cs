using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentorApp.DTOs.Requests
{
    public class EditProjectPromotersDTO
    {
        public int IdProject { get; set; }
        public Boolean  IsRemovePromoter { get; set; }
        public List<String> RemovedPromotersEmail { get; set; }
        public List<String> SupervisorEmails { get; set; }
    }
}
