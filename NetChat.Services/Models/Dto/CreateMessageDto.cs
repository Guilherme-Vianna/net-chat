using System;
using System.Collections.Generic;
using System.Text;

namespace NetChat.Services.Models.Dto
{
    public class CreateMessageDto
    {
        public Guid sender_id { get; private set; }
        public Guid receiver_id { get; set; }
        public string content { get; set; }

        public void InsertSenderId(Guid senderId)
        {
            sender_id = senderId;
        }
    }
}
