using NetChat.Services.Models.Dto;

namespace NetChat.Services.Interfaces;

public interface IChatService
{
    public Task<ChatResponseDto> GetChat(List<Guid> tags_ids, Guid user_id);
}