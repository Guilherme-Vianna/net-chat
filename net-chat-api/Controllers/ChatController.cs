using Microsoft.AspNetCore.Mvc;
using NetChat.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace net_chat_api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ChatController(IChatService service) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetChat([FromQuery] List<Guid> tags_ids)
        {
            var userId = Guid.Parse(HttpContext.User.Claims
                .First(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value
            );
            var result = await service.GetChat(tags_ids,userId);
            return Ok(result);
        }
    }
}