using System;
using System.Collections.Generic;

namespace Backend.Models
{
    public partial class Comment
    {
        public int IdComment { get; set; }
        public int Post { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public string Comment1 { get; set; }
    }
}
