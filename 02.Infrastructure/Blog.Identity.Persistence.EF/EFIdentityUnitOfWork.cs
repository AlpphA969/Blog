using Blog.Identity.Persistence.EF.Contracst;

namespace Blog.Identity.Persistence.EF;

public class EFIdentityUnitOfWork : IIdentityUnitOfWork
{
    
    private readonly EFIdentityDataContext _context;
    public EFIdentityUnitOfWork(EFIdentityDataContext context)
    {
        _context = context;
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }
}