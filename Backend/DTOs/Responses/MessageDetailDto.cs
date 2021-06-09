using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MentorApp.Models;
using MentorApp.Wrappers;

namespace MentorApp.DTOs.Responses
{
    public class MessageDetailDto
    {
        public UserWrapper SenderUser { get; set; }
        public UserWrapper ReceiverUser { get; set; }
        public List<MessageDto> Messages { get; set; }
    }

    public class MessageDto
    {
        public int IdMessage { get; set; }
        public int Receiver { get; set; }
        public int Sender { get; set; }
        public string Message1 { get; set; }
        public DateTime CreatedOn { get; set; }
    }

}
