using Blog.Entities;
using Services.Posts.Contracts.Dto;

namespace Services.Posts.Contracts;

public interface IPostTagRepository 
{
    Task AddPostTagAsync(
        PostTag postTag ,
        CancellationToken cancellationToken);
    
    Task<List<Guid>> GetTagsAsync(Guid postid, CancellationToken cancellationToken);
    
}