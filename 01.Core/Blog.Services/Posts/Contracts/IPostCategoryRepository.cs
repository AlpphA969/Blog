using Blog.Entities;

namespace Services.Posts.Contracts;

public interface IPostCategoryRepository
{
    Task<bool> PostExistAsync(Guid postId,
        CancellationToken cancellationToken);

    Task AddPostCategoryAsync(PostCategory postCategory,
        CancellationToken cancellationToken);
    
    


}