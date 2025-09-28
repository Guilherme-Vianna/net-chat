namespace NetChat.Services.Models.Dto;

public class CreateMessageDto
{
    public string message { get; set; }
    public Guid destination_id { get; set; }
    public Guid sender_id { get; set; }
}