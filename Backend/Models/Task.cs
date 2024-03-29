﻿using System;
using System.Collections.Generic;

namespace MentorApp.Models
{
    public partial class Task
    {
        public Task()
        {
            TaskAssigning = new HashSet<TaskAssigning>();
        }

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

        public virtual User CreatorNavigation { get; set; }
        public virtual Project ProjectNavigation { get; set; }
        public virtual TaskStatus StatusNavigation { get; set; }
        public virtual ICollection<TaskAssigning> TaskAssigning { get; set; }
    }
}
