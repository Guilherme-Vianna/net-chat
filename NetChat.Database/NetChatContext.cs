using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NetChat.Models;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;

namespace NetChat.Database
{
    public class NetChatContext : DbContext
    {
        public NetChatContext(DbContextOptions<NetChatContext> options)
        : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(NetChatContext).Assembly);
        }

        public DbSet<Message> Messages { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserTag> UserTags { get; set; }
        public DbSet<UserFriend> UserFriends { get; set; }
    }
}
