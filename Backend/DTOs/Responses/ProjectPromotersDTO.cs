using MentorApp.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentorApp.DTOs.Responses
{
    public class ProjectPromotersDTO
    {
        public int IdProject { get; set; }
        public UserWrapper MainMentor { get; set; }
        public List<UserWrapper> AdditionalMentors { get; set; }
    }
}
