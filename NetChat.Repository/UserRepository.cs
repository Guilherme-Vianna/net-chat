using Microsoft.EntityFrameworkCore;
using NetChat.Database;
using NetChat.Models;
using NetChat.Repository.Interfaces;
using System;
using System.Collections.Immutable;

namespace NetChat.Repository
{
    public class UserRepository(NetChatContext context) : BaseRepository(context), IUserRepository
    {
        public async Task<List<User>> GetUsersAsync() 
        {
            return await context.Users.ToListAsync();
        }

        // TODO: Pensar em algum algoritmo de conjuntos
        public async Task<List<Guid>> GetUsersIdsByTags(List<Guid> tags_ids, Guid userId)
        {
            var userIds = await context
                .Users
                .Include(x => x.Tags)
                .Where(x => x.Tags.Select(x =>x.TagId)
                    .Any(x => tags_ids.Contains(x)))
                .Where(x => x.Id != userId)
                .Select(x => x.Id)
                .ToListAsync();

            return userIds;
        }

        public async Task<User> CreateUser(User user)
        {
            var registered = await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
            return registered.Entity;
        }

        public async Task<bool> ExistEmail(string email)
        {
            return await context
                .Users
                .AsNoTracking()
                .AnyAsync(u => u.Email == email);
        }
        public async Task<bool> ExistEmail(string email, Guid userId)
        {
            return await context
                .Users
                .AsNoTracking()
                .AnyAsync(u => u.Email == email && u.Id != userId);
        }

        public async Task<User?> GetUserByIdAsync(Guid id)
        {
            return await context
                .Users
                .AsNoTracking()
                .Include(x => x.Tags)
                .ThenInclude(x => x.Tag)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User?> GetUserByIdToEditAsync(Guid id)
        {
            return await context
                .Users
                .Include(x => x.Tags)
                .ThenInclude(x => x.Tag)
                .Include(x => x.Messages)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task Update(User user)
        {
            context.Users.Update(user); 
            await context.SaveChangesAsync();
        }

        public async Task<User?> GetUserByIdAsyncWithTags(Guid id)
        {
            return await context
                .Users
                .AsNoTracking()
                .Include(x => x.Tags)
                .ThenInclude(x => x.Tag)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
