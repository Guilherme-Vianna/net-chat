using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using NetChat.Models;
using NetChat.Services.Interfaces;
using NetChat.Services.Models.Dto;
using System.Net.WebSockets;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace net_chat_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MessagesController(IMessageService service) : ControllerBase
    {
        private static Dictionary<Guid, WebSocket> connected_users = new();

        public async Task Get()
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
                var userId = Guid.Parse(User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
                if (!connected_users.ContainsKey(userId))
                {
                    connected_users.Add(userId, webSocket);
                }
                await MessageSend(webSocket);
            }
            else
            {
                HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            }
        }

        private async Task MessageSend(WebSocket webSocket)
        {
            const int bufferSize = 5 * 1024 * 1024;
            var buffer = new byte[bufferSize];
            var receiveResult = await webSocket.ReceiveAsync(
                new ArraySegment<byte>(buffer), CancellationToken.None);

            while (!receiveResult.CloseStatus.HasValue)
            {
                receiveResult = await webSocket.ReceiveAsync(
                    new ArraySegment<byte>(buffer), CancellationToken.None);

                var message = Encoding.UTF8.GetString(buffer, 0, receiveResult.Count);
                var messageDto = JsonSerializer.Deserialize<CreateMessageDto>(message);
                var userId = Guid.Parse(User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);

                messageDto?.InsertSenderId(userId);
                await service.CreateMessage(messageDto!);

                var serializedMessage = JsonSerializer.Serialize(messageDto);
                var messageBytes = Encoding.UTF8.GetBytes(serializedMessage);
                var messageSegment = new ArraySegment<byte>(messageBytes);

                foreach (var (connectedUserId, socket) in connected_users)
                {
                    if (connectedUserId == messageDto.receiver_id)
                    {
                        await socket.SendAsync(messageSegment, WebSocketMessageType.Text, true, CancellationToken.None);
                    }
                }
            }

            connected_users.Remove(Guid.Parse(User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value));

            await webSocket.CloseAsync( 
                receiveResult.CloseStatus.Value,
                receiveResult.CloseStatusDescription,
                CancellationToken.None);
        }
    }
}
