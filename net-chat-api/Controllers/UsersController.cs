using Microsoft.AspNetCore.Mvc;
using NetChat.Services.Interfaces;
using NetChat.Services.Models.UpdateDto;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using NetChat.Services.Models.Dto;

namespace net_chat_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(IUserService service) : ControllerBase
    {
        [Authorize]
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await service.GetUser(id);
            return Ok(result);
        }
        
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateUserDto dto)
        {
            var result = await service.CreateAsync(dto);
            return Ok(result);
        }
        
        [Authorize]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUserDto dto)
        {
            dto.id = id;
            var result = await service.UpdateAsync(dto);
            return Ok(result);
        }
        
        [Authorize]
        [HttpPut("password-update/{id:guid}")]
        public async Task<IActionResult> UpdatePassword(Guid id, [FromBody] UpdatePasswordDto dto)
        {
            dto.id = id;
            var result = await service.UpdatePasswordAsync(dto);
            return Ok(result);
        }
    }
}
