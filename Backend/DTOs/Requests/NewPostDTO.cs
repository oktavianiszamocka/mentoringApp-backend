using System;
using System.Collections.Generic;

namespace MentorApp.DTOs.Requests
{
    public class NewPostDTO
    {


        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime DateOfPublication { get; set; }
        public int Writer { get; set; }
        public int? Project { get; set; }
        public string Attachment { get; set; }
        public List<string> Tags { get; set; }
    }
}
