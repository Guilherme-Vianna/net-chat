using NetChat.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetChat.Repository.Interfaces
{
    public interface IUserRepository : IBaseRepository
    {
        public Task<User> CreateUser(User user);
        public Task<User?> GetUserByIdWithTagsAsync(Guid id);
        public Task<User?> GetUserByIdAsync(Guid id);
        public Task<User?> GetUserByIdToEditAsync(Guid id);
        public Task Update(User user);
        public Task<User?> GetUserByIdAsyncWithTags(Guid id);
        public Task<User?> GetUserByEmailAsync(string email);
        public Task<bool> ExistEmail(string email, Guid user_id);
        public Task<bool> ExistEmail(string email);
        public Task<User?> GetUserThatHaveTagList(List<Guid> tags_ids, int minMatchTags, Guid user_id);
        public Task<UserFriend> AddUserFriend(Guid userId, Guid friendId);
        public Task<List<UserFriend>> GetUserFriendsQueryable(Guid userId);
    }
}
