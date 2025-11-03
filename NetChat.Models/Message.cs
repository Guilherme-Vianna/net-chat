using NetChat.Models.Base;

namespace NetChat.Models
{
    public class Message : AuditEntity
    {
        public Message()
        {

        }

        public Message(string data, Guid senderId, Guid recipientId)
        {
            Data = data;
            SenderId = senderId;
            RecipientId = recipientId;
        }

        public string Data { get; set; }
        public User Sender { get; set; }
        public Guid SenderId { get; set; }
        public User Recipient { get; set; }
        public Guid RecipientId { get; set; }
    }
}