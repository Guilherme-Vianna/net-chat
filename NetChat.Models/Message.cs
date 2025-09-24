using NetChat.Models.Base;

namespace NetChat.Models
{
    public class Message : AuditEntity
    {
        public Message()
        {

        }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Name { get; set; }
        public User Sender { get; set; }
        public Guid SenderId { get; set; }
        public User Recipient { get; set; }
        public Guid RecipientId { get; set; }
    }
}