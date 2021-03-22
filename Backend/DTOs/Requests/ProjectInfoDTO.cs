using System;
using System.Collections.Generic;

namespace MentorApp.DTOs.Requests
{
    public class ProjectInfoDTO
    {
        public int IdProject { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string StatusName { get; set; }
        public string SuperviserFirstName { get; set; }
        public string SuperviserLastName { get; set; }
        public string Icon { get; set; }
        public string projectLeaderFirstName { get; set; }
        public string projectLeaderLastName { get; set; }
        public List<string> UrlLinks { get; set; }
    }
}
