using Blog.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.PersistenceEF.Post;

public class EFPostTagEntityMap : IEntityTypeConfiguration<PostTag>
{
    public void Configure(EntityTypeBuilder<PostTag> builder)
    {
        builder.ToTable("PostTags");

        builder.HasKey(pt => pt.Id);
        
        builder.Property(p => p.Id).ValueGeneratedOnAdd();

        builder.HasOne(pt=>pt.Tag)
            .WithMany(pt=>pt.PostTags)
            .HasForeignKey(pt => pt.TagId)
            .IsRequired();
        
        builder.HasOne(c => c.Post)
            .WithMany()
            .HasForeignKey(c => c.PostId)
            .IsRequired();
    }
}