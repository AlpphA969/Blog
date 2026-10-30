using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.PersistenceEF.Category;

public class EFCategoryEntityMap : IEntityTypeConfiguration<Entities.Category>
{
    public void Configure(EntityTypeBuilder<Entities.Category> builder)
    {
        builder.ToTable("Categories");
        
        builder.HasKey(c => c.Id);
        
        builder.Property(c=>c.Id).ValueGeneratedOnAdd();
        
        builder.Property(c=>c.Title)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.HasIndex(c=>c.Title).IsUnique();
        
    }
}