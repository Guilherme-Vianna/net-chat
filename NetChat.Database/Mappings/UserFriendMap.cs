using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetChat.Models;

public class UserFriendMap : IEntityTypeConfiguration<UserFriend>
{
    public void Configure(EntityTypeBuilder<UserFriend> builder)
    {
        builder.HasOne(uf => uf.User)
               .WithMany(u => u.Friends)
               .HasForeignKey(uf => uf.UserId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(uf => uf.Friend)
               .WithMany()
               .HasForeignKey(uf => uf.FriendId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.ToTable("user_friends");
    }
}