using Microsoft.EntityFrameworkCore;
using Services.Tags.Contracts;

namespace Blog.PersistenceEF.Tag;

public class EFTagRepository :ITagRepository
{
    private readonly DbSet<Entities.Tag> _tags;

    public EFTagRepository(EFDataContext context)
    {
        _tags = context.Set<Entities.Tag>();
    }
    public async Task<bool> TagsValidation(HashSet<Guid> tags,
        CancellationToken cancellationToken)
    {
        var result = await _tags.Where(t=> tags.Contains(t.Id))
            .ToListAsync(cancellationToken);
        if (result.Count != tags.Count)
        {
            return false;
        }

        return true;

    }
}

