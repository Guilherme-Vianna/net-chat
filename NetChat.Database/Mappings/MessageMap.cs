using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetChat.Models;
using System.Reflection.Metadata;


public class MessageMap : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.ToTable("messages");

        builder
            .HasOne(x => x.Sender)
            .WithMany()
            .HasForeignKey(x => x.SenderId);

        builder
            .HasOne(x => x.Recipient)
            .WithMany()
            .HasForeignKey(x => x.RecipientId);
    }
}