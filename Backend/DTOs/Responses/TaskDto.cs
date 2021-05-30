using System;
using System.Collections.Generic;
using MentorApp.Wrappers;

namespace MentorApp.DTOs.Responses
{
    public class TaskDto
    {
        public int IdTask { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
        public DateTime? ExpectedEndDate { get; set; }
        public DateTime? StartDate { get; set; }
        public int Status { get; set; }
        public String StatusName { get; set; }
        public DateTime? ActualEndDate { get; set; }
        public int Project { get; set; }
        public int Creator { get; set; }
        public UserWrapper CreatorUser { get; set; }
        public DateTime CreatedOn { get; set; }
        public List<UserWrapper> AssignedUser { get; set; }
    }
}
