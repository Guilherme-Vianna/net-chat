using Microsoft.Extensions.Logging;
using NetChat.Models;
using NetChat.Repository.Interfaces;
using NetChat.Services.Interfaces;
using NetChat.Services.Models.Dto;
using NetChat.Services.Models.ViewModels;

namespace NetChat.Services;

public class MessageService(IMessageRepository messageRepository) : IMessageService
{
    public async Task<MessageViewModel> CreateMessage(CreateMessageDto dto)
    {
        var newMessage = new Message(dto.message, senderId: dto.sender_id, recipientId: dto.destination_id);
        var messageRegister = await messageRepository.CreateMessageAsync(newMessage);
        var result = new MessageViewModel(messageRegister.Text, senderId: messageRegister.SenderId,
            destinationId: messageRegister.RecipientId, createdAt: messageRegister.CreatedAt);
        return result;
    }
    
    public async Task<List<MessageViewModel>> GetMessages(Guid sender_id, int page, int page_size)
    {
        var messages = await messageRepository.GetMessagesAsync(page, page_size,sender_id);
        var result = new List<MessageViewModel>();
        foreach (var message in messages)
        {
            var viewMessage = new MessageViewModel(message.Text, senderId: message.SenderId,
                destinationId: message.RecipientId, createdAt: message.CreatedAt);
            result.Add(viewMessage);
        }
        return result;
    }
}