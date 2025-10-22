using Microsoft.AspNetCore.Mvc;
using NetChat.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using NetChat.Services.Models.Dto;

namespace net_chat_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatsController(IChatService service) : ControllerBase
    {
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> GetNewChat([FromBody] SearchChatDto dto)
        {
            var userId = User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var result = await service.GetNewChat(dto.tags, Guid.Parse(userId));
            return Ok(result);
        }
    }
}
