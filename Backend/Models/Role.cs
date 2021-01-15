﻿
using System.Collections.Generic;

namespace MentorApp.Models
{
    public partial class Role
    {
        public Role()
        {
            User = new HashSet<User>();
        }

        public int IdRole { get; set; }
        public string Name { get; set; }

        public virtual ICollection<User> User { get; set; }
    }
}