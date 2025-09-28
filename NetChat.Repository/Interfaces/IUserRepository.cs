using NetChat.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetChat.Repository.Interfaces
{
    public interface IUserRepository : IBaseRepository
    {
        public Task<User> CreateUser(User user);
        public Task<List<Guid>> GetUsersIdsByTags(List<Guid> tags_ids, Guid user_id);
        public Task<User?> GetUserByIdAsync(Guid id);
        public Task<User?> GetUserByIdToEditAsync(Guid id);
        public Task Update(User user);
        public Task<User?> GetUserByIdAsyncWithTags(Guid id);
        public Task<User?> GetUserByEmailAsync(string email);
        public Task<bool> ExistEmail(string email, Guid userId);
        public Task<bool> ExistEmail(string email);
    }
}
