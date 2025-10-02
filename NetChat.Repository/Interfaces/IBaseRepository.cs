using System;
using System.Collections.Generic;
using System.Text;

namespace NetChat.Repository.Interfaces
{
    public interface IBaseRepository
    {
        Task SaveChanges();
        Task StartTransaction();
        Task RollBackTransaction();
        Task CommitTransaction();
    }
}
