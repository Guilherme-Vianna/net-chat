using NetChat.Services.Models.Dto;
using NetChat.Services.Models.ViewModels;

namespace NetChat.Services.Interfaces
{
    public interface IMessageService
    {
        public Task<MessageViewModel> CreateMessage(CreateMessageDto dto);
    }
}
