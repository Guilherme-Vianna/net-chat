using NetChat.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetChat.Repository.Interfaces
{
    public interface IUserRepository
    {
        public Task<User> CreateUser(User user);
        public Task<bool> ExistEmail(string email);
    }
}
