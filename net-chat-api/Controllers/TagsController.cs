using Microsoft.AspNetCore.Mvc;
using NetChat.Services.Interfaces;
using NetChat.Services.Models.CreateDto;
using System.Threading.Tasks;

namespace net_chat_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController(ITagService service) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateTagDto dto)
        {
            var result = await service.CreateAsync(dto);
            return Ok(result);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTagDto dto)
        {
            var result = await service.UpdateAsync(id, dto);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> Post([FromQuery] int page, [FromQuery] int page_size)
        {
            var result = await service.GetTagsAsync(page, page_size);
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await service.GetTag(id);
            return Ok(result);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await service.DeleteTag(id);
            return Ok();
        }
    }
}
