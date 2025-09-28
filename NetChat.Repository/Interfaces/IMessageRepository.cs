using NetChat.Models;

namespace NetChat.Repository.Interfaces;

public interface IMessageRepository
{
    public Task<Message> CreateMessageAsync(Message message);
    public Task<List<Message>> GetMessagesAsync(int page, int page_size, Guid sender_id);
}