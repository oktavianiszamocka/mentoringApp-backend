using System;
using System.Collections.Generic;
using MentorApp.DTOs.Responses;
using MentorApp.Models;

namespace MentorApp.DTOs.Requests
{
    public class ProjectInfoDTO
    {
        public int IdProject { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int Status { get; set; }
        public string StatusName { get; set; }
        public int? Studies { get; set; }

        public string StudiesName { get; set; }
        public int? Mode { get; set; }
        public string ModeName { get; set; }
        public int? Superviser { get; set; }
        public string SuperviserEmail { get; set; }
        public string SuperviserFirstName { get; set; }
        public string SuperviserLastName { get; set; }
        public string Icon { get; set; }
        public string projectLeaderFirstName { get; set; }
        public string projectLeaderLastName { get; set; }
        public List<UrlDTO> UrlLinks { get; set; }
    }
}
