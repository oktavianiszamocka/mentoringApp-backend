using System;
using System.Collections.Generic;
using MentorApp.Models;

namespace MentorApp.Wrappers
{
    public class PostWrapper
    {
        public int IdPost { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime DateOfPublication { get; set; }
        public UserWrapperWithSemesterAndMajor Writer { get; set; }
        public virtual CommentWrapper NewestComment { get; set; }
        public Boolean hasMoreThanOneComment { get; set; }
        public virtual ICollection<String> tags { get; set; }
    }

    public class UserWrapper
    {
        public int IdUser { get; set; }
        public String firstName { get; set; }
        public String lastName { get; set; }
        public String imageUrl { get; set; }
    }

    public class UserWrapperWithSemesterAndMajor
    {
        public int IdUser { get; set; }
        public String firstName { get; set; }
        public String lastName { get; set; }
        public String imageUrl { get; set; }
        public String semester { get; set; }
        public String major { get; set; }
    }

    public class CommentWrapper
    {

        public int IdComment { get; set; }
        public UserWrapper CreatedBy { get; set; }
        public String Comment { get; set; }
        public DateTime CreatedOn { get; set; }


    }
}

