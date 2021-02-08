﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentorApp.DTOs.Requests
{
    public class ProjectInfoDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string StatusName { get; set; }
        public string SuperviserFirstName { get; set; }
        public string SuperviserLastName { get; set; }
        public string Icon { get; set; }
    }
}
