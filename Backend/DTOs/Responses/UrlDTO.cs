using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentorApp.DTOs.Responses
{
    public class UrlDTO
    {
        public int IdUrl { get; set; }
        public string Link { get; set; }
        public int Project { get; set; }
        public int Type { get; set; }
    }
}
