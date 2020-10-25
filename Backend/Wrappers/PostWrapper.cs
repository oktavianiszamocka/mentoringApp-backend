using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentorApp.Wrappers
{
    public class PostWrapper
    {
        public int IdPost { get; set; }
        public string Content { get; set; }
        public DateTime DateOfPublication { get; set; }
        public UserWrapper Writer { get; set; }
    }

    public class UserWrapper
    {
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String AvatarURL { get; set; }
    }
}

