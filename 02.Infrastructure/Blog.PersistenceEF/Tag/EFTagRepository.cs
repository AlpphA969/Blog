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
    public async Task<bool> TagsValidation(List<Guid> tags,
        CancellationToken cancellationToken)
    {
        var distinctTags =  tags.Distinct().ToList();
        var result = await _tags.Where(t=> distinctTags.Contains(t.Id))
            .ToListAsync(cancellationToken);
        if (result.Count != distinctTags.Count)
        {
            return false;
        }

        return true;

    }
}

