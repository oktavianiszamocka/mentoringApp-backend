using System;
using System.Collections.Generic;

namespace Backend.Models
{
    public partial class Task
    {
        public int IdTask { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ExpectedEndDate { get; set; }
        public DateTime ActualEndDate { get; set; }
        public DateTime StartDate { get; set; }
        public string Status { get; set; }
        public int Project { get; set; }
        public int Creator { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}
