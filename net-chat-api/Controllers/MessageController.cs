using Microsoft.AspNetCore.Mvc;
using NetChat.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using NetChat.Services.Models.Dto;

namespace net_chat_api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class MessageController(IMessageService service) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateMessageDto dto)
        {
            var result = await service.CreateMessage(dto);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int page, [FromQuery] int page_size)
        {
            var result = await service.GetMessages(page, page_size);
            return Ok(result);
        }
    }
}