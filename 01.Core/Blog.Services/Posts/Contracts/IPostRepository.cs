using Blog.Entities;
using Services.Posts.Contracts.Dto;

namespace Services.Posts.Contracts;

public interface IPostRepository
{
    Task<Guid> AddPostAsync(Post post , CancellationToken cancellationToken);

    Task<bool> PostExistAsync(Guid id ,  CancellationToken cancellationToken);
    
    Task<GetPostDto?> GetPostAsync(Guid id,
        CancellationToken cancellationToken);

}