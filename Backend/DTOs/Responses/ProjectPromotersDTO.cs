using MentorApp.Wrappers;
using System.Collections.Generic;

namespace MentorApp.DTOs.Responses
{
    public class ProjectPromotersDTO
    {
        public int IdProject { get; set; }
        public UserWrapper MainMentor { get; set; }
        public List<UserWrapper> AdditionalMentors { get; set; }
    }
}
