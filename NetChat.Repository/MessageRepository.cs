using Microsoft.EntityFrameworkCore;
using NetChat.Database;
using NetChat.Models;
using NetChat.Repository.Interfaces;

namespace NetChat.Repository;

public class MessageRepository(NetChatContext context) : IMessageRepository
{
    public async Task<Message> CreateMessageAsync(Message message)
    {
        var entity = await context.Messages.AddAsync(message);
        await context.SaveChangesAsync();
        return entity.Entity;
    }

    public async Task<List<Message>> GetMessagesAsync(int page, int page_size)
    {
        if (page < 1) page = 1;
        if (page_size < 1) page_size = 10; 

        return await context.Messages
            .AsNoTracking()
            .OrderByDescending(x => x.CreatedAt)
            .Skip((page - 1) * page_size)
            .Take(page_size)
            .ToListAsync();
    }
}