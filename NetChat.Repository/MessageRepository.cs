using NetChat.Database;
using NetChat.Models;
using NetChat.Repository.Interfaces;

namespace NetChat.Repository
{
    public class MessageRepository(NetChatContext context) : BaseRepository(context), IMessageRepository
    {
        public async Task<Message> CreateMessage(Message message)
        {
            var newMessage = await context.AddAsync(message);
            await context.SaveChangesAsync();
            return newMessage.Entity;
        }
    }
}
