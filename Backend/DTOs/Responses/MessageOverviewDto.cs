using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MentorApp.Wrappers;

namespace MentorApp.DTOs.Responses
{
    public class MessageOverviewDto
    {
        public int SenderId { get; set; }
        public String Message { get; set; }
        public DateTime LastMessage { get; set; }
        public UserWrapper SenderUser { get; set; }
    }


}
