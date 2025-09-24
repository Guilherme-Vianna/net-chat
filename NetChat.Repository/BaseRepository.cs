using NetChat.Database;
using NetChat.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetChat.Repository
{
    public class BaseRepository(NetChatContext context)
    {
        public async Task StartTransaction()
        {
            await context.Database.BeginTransactionAsync();   
        }


        public async Task CommitTransaction()
        {
            await context.Database.CommitTransactionAsync();
        }
    }
}
