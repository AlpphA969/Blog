using Blog.Entities;
using Microsoft.EntityFrameworkCore;
using Services.Posts.Contracts;

namespace Blog.PersistenceEF.Post;

public class EFPostCategoryRepository : IPostCategoryRepository
{
    private readonly DbSet<PostCategory> _postCategores;

    public EFPostCategoryRepository(EFDataContext context)
    {
        _postCategores = context.Set<PostCategory>();
    }

    public Task<bool> PostExistAsync(Guid postId,
        CancellationToken cancellationToken)
    {
        var exists = _postCategores.Where(pc => pc.PostId == postId)
            .AnyAsync(cancellationToken);
        return exists;

    }

    public async Task AddPostCategoryAsync(PostCategory postCategory,
        CancellationToken cancellationToken)
    {
        await _postCategores.AddAsync(postCategory);

    }

    public async Task<List<Guid>> GetCategoriesAsync(Guid id, CancellationToken cancellationToken)
    {
        var categories = await _postCategores
            .Where(pc => pc.PostId == id)
            .Select(pc=>pc.CategoryId)
            .ToListAsync(cancellationToken);
        return categories;
    }
}
    