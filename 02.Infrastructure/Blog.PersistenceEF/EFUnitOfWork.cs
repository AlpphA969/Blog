using Microsoft.EntityFrameworkCore;
using Servisec;

namespace Blog.PersistenceEF;

public class EFUnitOfWork : IUnitOfWork
{
    private readonly EFDataContext _dbContext;

    public EFUnitOfWork(EFDataContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }
}