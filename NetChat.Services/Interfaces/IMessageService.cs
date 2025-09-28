using NetChat.Services.Models.Dto;
using NetChat.Services.Models.ViewModels;

namespace NetChat.Services.Interfaces;

public interface IMessageService
{
    public Task<MessageViewModel> CreateMessage(CreateMessageDto dto);
    public Task<List<MessageViewModel>> GetMessages(Guid sender_id, int page, int page_size);
}