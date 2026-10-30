using Blog.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Blog.Identity.Persistence.EF;

public class EFIdentityDataContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
{
    public EFIdentityDataContext(DbContextOptions<EFIdentityDataContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(EFIdentityDataContext).Assembly);
    }
}