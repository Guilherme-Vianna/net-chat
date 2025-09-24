using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetChat.Models;
using System.Reflection.Metadata;

public class UserTagMap : IEntityTypeConfiguration<UserTag>
{
    public void Configure(EntityTypeBuilder<UserTag> builder)
    {
        builder
            .ToTable("user_tags");

        builder.HasNoKey();

        builder.HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId);

        builder.HasOne(x => x.Tag)
           .WithMany()
           .OnDelete(DeleteBehavior.Restrict)
           .HasForeignKey(x => x.TagId);
    }
}