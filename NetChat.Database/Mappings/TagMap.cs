using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetChat.Models;
using System.Reflection.Metadata;

public class TagMap : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder
            .ToTable("tags");
        builder
            .HasIndex(x => x.Name);
    }
}