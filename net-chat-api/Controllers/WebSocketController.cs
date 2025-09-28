using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NetChat.Services.Extras;
using NetChat.Services.Interfaces;
using NetChat.Services.Models.Dto;

namespace net_chat_api.Controllers;

[Authorize]
public class WebSocketController(IServiceProvider serviceProvider) : ControllerBase
{
    public static Dictionary<Guid, WebSocket> WebSockets = new Dictionary<Guid, WebSocket>();
    public static object lockObject = new object(); 
    
    [Route("/ws")]
    public async Task Get()
    {
        if (HttpContext.WebSockets.IsWebSocketRequest)
        {
            using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
            await Process(webSocket);
        }
        else
        {
            HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        }
    }

    private async Task SendMessage(SocketOperationDto operation, Guid userId, WebSocket webSocket)
    {
        var messageJson = operation.data.ToString(); 
        var message = Serializator.Deserialize<CreateMessageDto>(messageJson);
        message.sender_id = userId;
        var messageCreation = await ServiceInstantiator.GetService<IMessageService>(serviceProvider).CreateMessage(message);
            
        var responseJson = JsonSerializer.Serialize(messageCreation); 
        var responseBytes = Encoding.UTF8.GetBytes(responseJson);
        await webSocket.SendAsync(
            new ArraySegment<byte>(responseBytes, 0, responseBytes.Length),
            WebSocketMessageType.Text, true, CancellationToken.None);

        WebSocket? listenerSocket = null;
        
        lock (lockObject)
        {
            if (WebSockets.ContainsKey(message.destination_id))
            {
                listenerSocket = WebSockets.First(x => x.Key == message.destination_id).Value;
            }    
        }

        if (listenerSocket != null)
        {
            await SendNotification(new CreateNotificationDto(message.message, message.sender_id), listenerSocket);
        }
    }
    
    private async Task SendNotification(CreateNotificationDto dto, WebSocket webSocket)
    {
        var responseJson = JsonSerializer.Serialize(dto); 
        var responseBytes = Encoding.UTF8.GetBytes(responseJson);
        await webSocket.SendAsync(
            new ArraySegment<byte>(responseBytes, 0, responseBytes.Length),
            WebSocketMessageType.Text, true, CancellationToken.None);
    }
    
    private async Task Process(WebSocket webSocket)
    {
        #region Register Socket

        var userId = Guid.Parse(HttpContext.User.Claims
            .First(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value
        );

        lock (lockObject)
        {
            if (WebSockets.ContainsKey(userId))
            {
                WebSockets.Remove(userId);
            }
            
            WebSockets.Add(userId, webSocket);
        }

        #endregion
       
        var readBuffer = new byte[1024 * 4];
        var receiveResult = await webSocket.ReceiveAsync(
            new ArraySegment<byte>(readBuffer), CancellationToken.None);
        
        while (!receiveResult.CloseStatus.HasValue)
        {

            var operationEncoded = Encoding.UTF8.GetString(readBuffer, 0, receiveResult.Count);
            var operation = Serializator.Deserialize<SocketOperationDto>(operationEncoded);

            if (operation.operation == SocketOperation.SEND_MESSAGE)
            {
                await SendMessage(operation, userId, webSocket);
            }
            else
            {
                await webSocket.SendAsync(
                    new ArraySegment<byte>(readBuffer, 0, receiveResult.Count),
                    receiveResult.MessageType,
                    receiveResult.EndOfMessage,
                    CancellationToken.None);
            }

            receiveResult = await webSocket.ReceiveAsync(
                new ArraySegment<byte>(readBuffer), CancellationToken.None);
        }

        await webSocket.CloseAsync(
            receiveResult.CloseStatus.Value,
            receiveResult.CloseStatusDescription,
            CancellationToken.None);
    }
}