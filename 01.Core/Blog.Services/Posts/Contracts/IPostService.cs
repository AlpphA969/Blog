using Blog.Entities;
using Services.Categories.Dto;
using Services.Contracts;
using Services.Posts.Contracts.Dto;

namespace Services.Posts.Contracts;

public interface IPostService
{
    Task<Result<AddPostResultDto>> CreatePostAsync(AddPostDto dto,
        CancellationToken cancellationToken);
    
    Task<Result<GetPostDto>> GetPostAsync(Guid id,
        CancellationToken cancellationToken);
    
    Task<Result<Guid>>  UpdateTitleAsync(UpdateTitleDto dto,
        Guid id,
        CancellationToken cancellationToken);
    
    Task<Result<Guid>> UpdateFileAsync(UpdateFileDto dto, Guid id,
        CancellationToken cancellationToken);
    
    Task<Result<Guid>> UpdateCategoryAsync(
        UpdateCategoriesDto dto,
        Guid id,
        CancellationToken cancellationToken);

    Task<Result<Guid>> UpdateTagAsync(
        UpdateTagsDto dto,
        Guid id,
        CancellationToken cancellationToken);
    
    Task<Result<Guid>> PublishAsync(Guid id, CancellationToken cancellationToken);



}