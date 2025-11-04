using Microsoft.AspNetCore.Mvc;
using NetChat.Services.Interfaces;
using NetChat.Services.Models.UpdateDto;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using NetChat.Services.Models.Dto;
using System.Security.Claims;

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

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetData()
        {
            var userId = User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var result = await service.GetUser(Guid.Parse(userId));
            return Ok(result);
        }

        [Authorize]
        [HttpGet("friends")]
        public async Task<IActionResult> GetFriends()
        {
            var userId = User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var result = await service.GetFriends(Guid.Parse(userId));
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateUserDto dto)
        {
            var result = await service.CreateAsync(dto);
            return Ok(result);
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateUserDto dto)
        {
            var userId = User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
            dto.id = Guid.Parse(userId);
            var result = await service.UpdateAsync(dto);
            return Ok(result);
        }
        
        [Authorize]
        [HttpPut("friends")]
        public async Task<IActionResult> AddFriend([FromBody] AddUserFriendDto dto)
        {
            dto.InsertUserId(Guid.Parse(User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value));
            await service.AddFriend(dto);
            return Ok();
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
