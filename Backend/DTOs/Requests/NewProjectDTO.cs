using System;

namespace MentorApp.DTOs.Requests
{
    public class NewProjectDTO
    {
        public int IdProject { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string SuperviserEmail { get; set; }
        public int Status { get; set; }
    }
}
