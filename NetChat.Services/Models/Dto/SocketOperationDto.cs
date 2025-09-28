namespace NetChat.Services.Models.Dto;

public class SocketOperationDto
{
    public string operation { get; set; }
    public object data { get; set; }
}

public static class SocketOperation
{
    public static string SEND_MESSAGE = "send_message";
    public static string RECEIVE_MESSAGE = "receive_message";
}