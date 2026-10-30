namespace Blog.Identity.Persistence.EF.Contracst;

public interface IIdentityUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    
}