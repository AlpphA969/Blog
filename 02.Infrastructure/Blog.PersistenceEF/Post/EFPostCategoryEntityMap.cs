using Blog.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.PersistenceEF.Post;

public class EFPostCategoryEntityMap : IEntityTypeConfiguration<PostCategory>
{
    public void Configure(EntityTypeBuilder<PostCategory> builder)
    {
        builder.ToTable("PostCategories");
        
        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.Id).ValueGeneratedOnAdd();

        builder.HasOne(c => c.Category)
            .WithMany(p=>p.PostCategories)
            .HasForeignKey(c => c.CategoryId)
            .IsRequired();
        
        builder.HasOne(p => p.Post)
            .WithMany(p=>p.PostCategories)
            .HasForeignKey(p => p.PostId)
            .IsRequired();
        


    }
}