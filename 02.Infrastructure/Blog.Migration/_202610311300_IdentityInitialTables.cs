using FluentMigrator;
namespace Blog.Migration;

[Migration(202610311310)]
public class _202610311300_IdentityInitialTables : FluentMigrator.Migration
{
    public override void Up()
    {
         Create.Table("AspNetRoles")
            .WithColumn("Id").AsGuid().NotNullable().PrimaryKey("PK_AspNetRoles")
            .WithColumn("Name").AsString(256).Nullable()
            .WithColumn("NormalizedName").AsString(256).Nullable()
            .WithColumn("ConcurrencyStamp").AsString().Nullable();

        Create.Table("AspNetUsers")
            .WithColumn("Id").AsGuid().NotNullable().PrimaryKey("PK_AspNetUsers")
            .WithColumn("UserName").AsString(256).Nullable()
            .WithColumn("NormalizedUserName").AsString(256).Nullable()
            .WithColumn("Email").AsString(256).Nullable()
            .WithColumn("NormalizedEmail").AsString(256).Nullable()
            .WithColumn("EmailConfirmed").AsBoolean().NotNullable()
            .WithColumn("PasswordHash").AsString().Nullable()
            .WithColumn("SecurityStamp").AsString().Nullable()
            .WithColumn("ConcurrencyStamp").AsString().Nullable()
            .WithColumn("PhoneNumber").AsString().Nullable()
            .WithColumn("PhoneNumberConfirmed").AsBoolean().NotNullable()
            .WithColumn("TwoFactorEnabled").AsBoolean().NotNullable()
            .WithColumn("LockoutEnd").AsDateTimeOffset().Nullable()
            .WithColumn("LockoutEnabled").AsBoolean().NotNullable()
            .WithColumn("AccessFailedCount").AsInt32().NotNullable();

        Create.Table("AspNetRoleClaims")
            .WithColumn("Id").AsInt32().NotNullable().PrimaryKey("PK_AspNetRoleClaims").Identity()
            .WithColumn("RoleId").AsGuid().NotNullable()
                .ForeignKey("FK_AspNetRoleClaims_AspNetRoles_RoleId", "AspNetRoles", "Id")
            .WithColumn("ClaimType").AsString().Nullable()
            .WithColumn("ClaimValue").AsString().Nullable();

        Create.Table("AspNetUserClaims")
            .WithColumn("Id").AsInt32().NotNullable().PrimaryKey("PK_AspNetUserClaims").Identity()
            .WithColumn("UserId").AsGuid().NotNullable()
                .ForeignKey("FK_AspNetUserClaims_AspNetUsers_UserId", "AspNetUsers", "Id")
            .WithColumn("ClaimType").AsString().Nullable()
            .WithColumn("ClaimValue").AsString().Nullable();

        Create.Table("AspNetUserLogins")
            .WithColumn("LoginProvider").AsString().NotNullable()
            .PrimaryKey("PK_AspNetUserLogins")
            .WithColumn("ProviderKey").AsString().NotNullable()
            .PrimaryKey("PK_AspNetUserLogins")
            .WithColumn("ProviderDisplayName").AsString().Nullable()
            .WithColumn("UserId").AsGuid().NotNullable()
            .ForeignKey("FK_AspNetUserLogins_AspNetUsers_UserId", "AspNetUsers",
                "Id");

        Create.Table("AspNetUserRoles")
            .WithColumn("UserId").AsGuid().NotNullable()
            .PrimaryKey("PK_AspNetUserRoles")
            .ForeignKey("FK_AspNetUserRoles_AspNetUsers_UserId", "AspNetUsers",
                "Id")
            .WithColumn("RoleId").AsGuid().NotNullable()
            .PrimaryKey("PK_AspNetUserRoles")
            .ForeignKey("FK_AspNetUserRoles_AspNetRoles_RoleId", "AspNetRoles",
                "Id");

        Create.Table("AspNetUserTokens")
            .WithColumn("UserId").AsGuid().NotNullable().PrimaryKey("PK_AspNetUserTokens")
                .ForeignKey("FK_AspNetUserTokens_AspNetUsers_UserId", "AspNetUsers", "Id")
            .WithColumn("LoginProvider").AsString().NotNullable().PrimaryKey("PK_AspNetUserTokens")
            .WithColumn("Name").AsString().NotNullable().PrimaryKey("PK_AspNetUserTokens")
            .WithColumn("Value").AsString().Nullable();
    }

    public override void Down()
    {
        Delete.Table("AspNetRoleClaims");
        Delete.Table("AspNetUserClaims");
        Delete.Table("AspNetUserLogins");
        Delete.Table("AspNetUserRoles");
        Delete.Table("AspNetUserTokens");
        Delete.Table("AspNetRoles");
        Delete.Table("AspNetUsers");
    }
}