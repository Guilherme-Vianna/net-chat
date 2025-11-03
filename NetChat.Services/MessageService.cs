using NetChat.Database;
using NetChat.Models;
using NetChat.Repository;
using NetChat.Repository.Interfaces;
using NetChat.Services.Interfaces;
using NetChat.Services.Models.Dto;
using NetChat.Services.Models.ViewModels;

namespace NetChat.Services
{
    public class MessageService(IMessageRepository messageRepository) : IMessageService
    {
        public async Task<MessageViewModel> CreateMessage(CreateMessageDto dto)
        {
            await messageRepository.StartTransaction();
            var newMessage = new Message(dto.content, dto.sender_id, dto.receiver_id);
            await messageRepository.CreateMessage(newMessage);
            await messageRepository.CommitTransaction();
            return new MessageViewModel(
                newMessage.Id, 
                newMessage.Data, 
                newMessage.SenderId, 
                newMessage.RecipientId, 
                newMessage.CreatedAt
            );
        }
    }
}
