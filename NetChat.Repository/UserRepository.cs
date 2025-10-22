using Microsoft.EntityFrameworkCore;
using NetChat.Database;
using NetChat.Models;
using NetChat.Repository.Interfaces;
using System;
using System.Reflection.Metadata.Ecma335;

namespace NetChat.Repository
{
    public class UserRepository(NetChatContext context) : BaseRepository(context), IUserRepository
    {
        public async Task<List<User>> GetUsersAsync() 
        {
            return await context.Users.ToListAsync();
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
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User?> GetUserByIdToEditAsync(Guid id)
        {
            return await context
                .Users
                .Include(x =>x.Tags)
                .FirstOrDefaultAsync(x => x.Id == id);
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
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<User?> GetUserThatHaveTagList(List<Guid> tags_ids, int minMatchTags, Guid user_id)
        {
            var usersThatHaveAtLeastOneTag =
                context.UserTags.Where(x => tags_ids.Contains(x.TagId) && x.UserId != user_id).GroupBy(x => x.UserId);

            var usersScore = new List<Tuple<Guid, int>>();

            foreach (var user in usersThatHaveAtLeastOneTag)
            {
                var score = 0;
                foreach (var item in user.Select(x => x.TagId))
                {
                    if(tags_ids.Contains(item))
                    {
                        score++;
                    }
                }

                usersScore.Add(new Tuple<Guid, int>(user.Key, score));
            }

            return await GetUserByIdAsync(usersScore.OrderByDescending(x => x.Item2).Select(x => x.Item1).FirstOrDefault());
        }
    }
}
