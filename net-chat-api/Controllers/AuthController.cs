using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetChat.Services.Interfaces;
using NetChat.Services.Models.Dto;
using NetChat.Services.Models.UpdateDto;

namespace net_chat_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService service) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] LoginDto dto)
        {
            var result = await service.Login(dto);
            return Ok(result);
        }
        
        [Authorize]
        [HttpGet]
        public IActionResult VerifyAuth()
        {
            return NoContent();
        }
    }
}
