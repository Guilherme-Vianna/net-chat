using NetChat.Database;
using NetChat.Models;
using NetChat.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetChat.Repository
{
    public class BaseRepository(NetChatContext context) : IBaseRepository
    {
        public async Task SaveChanges()
        {
            await context.SaveChangesAsync();
        }

        public async Task StartTransaction()
        {
            await context.Database.BeginTransactionAsync();   
        }


        public async Task CommitTransaction()
        {
            await context.Database.CommitTransactionAsync();
        }

        public async Task RollBackTransaction()
        {
            await context.Database.RollbackTransactionAsync();
        }
    }
}
