using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.PersistenceEF.Tag;

public class EFTagEntityMap : IEntityTypeConfiguration<Entities.Tag>
{
    public void Configure(EntityTypeBuilder<Entities.Tag> builder)
    {
        builder.Property(t => t.Title).HasMaxLength(100).IsRequired();
    }
}