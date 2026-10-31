using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Blog.PersistenceEF.Post;

public class EFPostEntityMap : IEntityTypeConfiguration<Entities.Post>
{
    public void Configure(EntityTypeBuilder<Entities.Post> builder)
    {
        builder.ToTable("Posts");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id).ValueGeneratedOnAdd();
        
        builder.Property(c => c.Title)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(p => p.ImageUrl);
        
        builder.Property(p=>p.IsPublished);



    }
}