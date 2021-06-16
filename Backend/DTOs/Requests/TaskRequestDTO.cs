using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentorApp.DTOs.Requests
{
    public class TaskRequestDTO
    {
        public int IdTask { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
        public DateTime? ExpectedEndDate { get; set; }
        public DateTime? StartDate { get; set; }
        public int Status { get; set; }
        public DateTime? ActualEndDate { get; set; }
        public int Project { get; set; }
        public int Creator { get; set; }
        public DateTime CreatedOn { get; set; }

        public List<int> AssignedUsers { get; set; }
        public Boolean IsAddNewAssignee { get; set; }
        public Boolean IsRemoveAssignee { get; set; }
        public List<int> AssignedUsersToRemove { get; set; }
        public List<int> AssignedUsersToAdd { get; set; }
    }
}
