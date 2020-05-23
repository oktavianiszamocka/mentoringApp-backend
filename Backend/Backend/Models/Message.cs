using System;
using System.Collections.Generic;

namespace Backend.Models
{
    public partial class Message
    {
        public int IdMessage { get; set; }
        public int Receiver { get; set; }
        public int Sender { get; set; }
        public string Message1 { get; set; }
        public string Attachment { get; set; }

        public virtual User ReceiverNavigation { get; set; }
        public virtual User SenderNavigation { get; set; }
    }
}
