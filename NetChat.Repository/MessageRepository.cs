using Microsoft.EntityFrameworkCore;
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

        public async Task<Message?> GetLastMessage(Guid userId, Guid friendId)
        {
            var lastMessage = await context.Messages
                .Where(x => (x.SenderId == userId && x.RecipientId == friendId))
                .AsNoTracking()
                .OrderByDescending(x => x.CreatedAt)
                .FirstOrDefaultAsync();

            if(lastMessage == null)
            {
                return null;
            }

            return lastMessage;
        }
    }
}
