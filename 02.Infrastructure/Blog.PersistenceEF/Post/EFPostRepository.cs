using Microsoft.EntityFrameworkCore;
using Services.Posts.Contracts;
using Services.Posts.Contracts.Dto;

namespace Blog.PersistenceEF.Post;

public class EFPostRepository : IPostRepository
{
    
    private readonly DbSet<Entities.Post> _posts;

    public EFPostRepository(EFDataContext context)
    {
        _posts = context.Set<Entities.Post>();
    }

    public async Task<Guid> AddPostAsync(Entities.Post post ,
        CancellationToken cancellationToken = default)
    {
        var result = await _posts.AddAsync(post , cancellationToken);
        return result.Entity.Id;
        
    }

    public Task<bool> PostExistAsync(Guid id ,
        CancellationToken cancellationToken = default)
    {
        var exist = _posts.Where(p => p.Id == id)
            .AnyAsync(cancellationToken);
        return exist;
    }
    
    public async Task<GetPostDto?> GetPostAsync(Guid id, 
        CancellationToken cancellationToken)
    {
        var post = await _posts.Where(p => p.Id == id)
            .Select(p => new GetPostDto
            {
                Id = p.Id,
                Title = p.Title,
                IsPublished = p.IsPublished,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt,
                ImageUrl = p.ImageUrl,
                Categories = p.PostCategories
                    .Select(pc=>pc.CategoryId).ToList(),
                Tags = p.PostTags
                    .Select(pt=>pt.TagId).ToList()
                

            }).FirstOrDefaultAsync(cancellationToken);
        return post;
    }
    
    public async Task<Entities.Post?> GetPostByIdAsync(Guid id,
        CancellationToken cancellationToken)
    {
        var post = await _posts.Where(p => p.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
        return post;
    }
}