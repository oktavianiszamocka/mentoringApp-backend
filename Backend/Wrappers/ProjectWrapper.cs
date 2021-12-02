using System;

namespace MentorApp.Wrappers
{
    public class ProjectWrapper
    {
        public int IdProject { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? Superviser { get; set; }
        public string SuperviserFullName { get; set; }
        public string StatusName { get; set; }
        public string StudyName { get; set; }
        public string ModeName { get; set; }
    }
}
