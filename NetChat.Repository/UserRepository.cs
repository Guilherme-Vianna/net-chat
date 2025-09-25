using Microsoft.EntityFrameworkCore;
using NetChat.Database;
using NetChat.Models;
using NetChat.Repository.Interfaces;
using System;

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
            return await context.Users.AnyAsync(u => u.Email == email);
        }
    }
}
