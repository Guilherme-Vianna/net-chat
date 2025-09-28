namespace NetChat.Services.Models.Dto;

public class CreateNotificationDto
{
    public CreateNotificationDto(string message, Guid senderId)
    {
        this.message = message;
        sender_id = senderId;
    }
    public string message { get; set; }
    public Guid sender_id { get; set; }
}