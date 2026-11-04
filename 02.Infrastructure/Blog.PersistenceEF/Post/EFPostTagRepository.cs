using Blog.Entities;
using Microsoft.EntityFrameworkCore;
using Services.Posts.Contracts;

namespace Blog.PersistenceEF.Post;

public class EFPostTagRepository : IPostTagRepository
{
    
    private readonly DbSet<Entities.PostTag> _postTags;

    public EFPostTagRepository(EFDataContext context)
    {
        _postTags = context.Set<Entities.PostTag>();
    }
    
    public async Task AddPostTagAsync(PostTag postTag, CancellationToken cancellationToken)
    {
        await _postTags.AddAsync(postTag, cancellationToken);

    }

    public async Task<List<Guid>> GetTagsAsync(Guid postid, CancellationToken cancellationToken)
    {
        var tags = await _postTags
            .Where(pt => pt.PostId == postid)
            .Select(pt=> pt.TagId)
            .ToListAsync(cancellationToken);
        return tags;
    }
}