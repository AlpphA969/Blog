using FluentMigrator;
namespace Blog.Migration;
[Migration(202610311105)]
public class _202610311310_BlogInitialTables : FluentMigrator.Migration
{
    public override void Up()
    {
        Create.Table("Categories")
            .WithColumn("Id").AsGuid().PrimaryKey()
            .WithColumn("Title").AsString(50).NotNullable().Unique()
            .WithColumn("CreatedAt").AsDateTimeOffset().NotNullable()
            .WithColumn("UpdatedAt").AsDateTimeOffset().Nullable();
        
        
        Create.Table("Tags")
            .WithColumn("Id").AsGuid().PrimaryKey()
            .WithColumn("Title").AsString(100).NotNullable()
            .WithColumn("CreatedAt").AsDateTimeOffset().NotNullable()
            .WithColumn("UpdatedAt").AsDateTimeOffset().Nullable();

        
        Create.Table("Posts")
            .WithColumn("Id").AsGuid().PrimaryKey()
            .WithColumn("Title").AsString(100).NotNullable()
            .WithColumn("ImageUrl").AsString().Nullable()
            .WithColumn("IsPublished").AsBoolean().NotNullable()
            .WithColumn("CreatedAt").AsDateTimeOffset().NotNullable()
            .WithColumn("UpdatedAt").AsDateTimeOffset().Nullable();

        Create.Table("PostTags")
            .WithColumn("Id").AsGuid().PrimaryKey()
            .WithColumn("PostId").AsGuid().NotNullable()
            .ForeignKey("FK_PostTags_Posts", "Posts", "Id")

            .WithColumn("TagId").AsGuid().NotNullable()
            .ForeignKey("FK_PostTags_Tags", "Tags", "Id")
            
            .WithColumn("CreatedAt").AsDateTimeOffset().NotNullable()
            .WithColumn("UpdatedAt").AsDateTimeOffset().Nullable();
        
        
        Create.Table("PostCategories")
            .WithColumn("Id").AsGuid().PrimaryKey()
            .WithColumn("PostId").AsGuid().NotNullable()
            .ForeignKey("FK_PostCategories_Posts", "Posts", "Id")
            
            .WithColumn("CategoryId").AsGuid().NotNullable()
            .ForeignKey("FK_PostCategories_Categories", "Categories", "Id")
            
            .WithColumn("CreatedAt").AsDateTimeOffset().NotNullable()
            .WithColumn("UpdatedAt").AsDateTimeOffset().Nullable();
        
        
    }

    public override void Down()
    {
        Delete.Table("PostCategories");
        Delete.Table("PostTags");
        Delete.Table("Posts");
        Delete.Table("Tags");
        Delete.Table("Categories");
    }
}