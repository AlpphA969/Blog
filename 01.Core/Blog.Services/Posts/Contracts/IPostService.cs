using Blog.Entities;
using Services.Contracts;
using Services.Posts.Contracts.Dto;

namespace Services.Posts.Contracts;

public interface IPostService
{
    Task<Result<AddPostResultDto>> CreatePostAsync(AddPostDto dto,
        CancellationToken cancellationToken);
    
    Task<Result<GetPostDto>> GetPostAsync(Guid id,
        CancellationToken cancellationToken);
    
}