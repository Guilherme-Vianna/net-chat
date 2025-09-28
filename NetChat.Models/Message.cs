using NetChat.Models.Base;

namespace NetChat.Models
{
    public class Message : AuditEntity
    {
        public Message()
        {

        }

        public Message(string text, Guid senderId,  Guid recipientId)
        {
            Text = text;
            SenderId = senderId;
            RecipientId = recipientId;
        }
        public string Text { get; set; }
        public User Sender { get; set; }
        public Guid SenderId { get; set; }
        public User Recipient { get; set; }
        public Guid RecipientId { get; set; }
    }
}