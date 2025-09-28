namespace NetChat.Services.Models.ViewModels;

public class MessageViewModel
{
    public MessageViewModel(string messageText, Guid senderId, Guid destinationId, DateTime createdAt)
    {
        message_text = messageText;
        sender_id = senderId;
        destination_id = destinationId;
        created_at = createdAt;
    }
    public string message_text { get; set; }
    public Guid sender_id { get; set; }
    public Guid destination_id { get; set; }
    public DateTime created_at { get; set; }
}