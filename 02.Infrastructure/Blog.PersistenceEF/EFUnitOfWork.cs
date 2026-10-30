using Microsoft.EntityFrameworkCore;
using Servisec;

namespace Blog.PersistenceEF;

public class EFUnitOfWork : IUnitOfWork
{
    private readonly DbContext _dbContext;

    public EFUnitOfWork(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }
}