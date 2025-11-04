using NetChat.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetChat.Repository.Interfaces
{
    public interface IMessageRepository : IBaseRepository
    {
        public Task<Message> CreateMessage(Message message);
        public Task<Message?> GetLastMessage(Guid userId, Guid friendId);
    }
}
