using Microsoft.AspNetCore.Mvc;
using NetChat.Services.Interfaces;
using NetChat.Services.Models.CreateDto;
using System.Threading.Tasks;

namespace net_chat_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(IUserService service) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateUserDto dto)
        {
            var result = await service.CreateAsync(dto);
            return Ok(result);
        }
    }
}
