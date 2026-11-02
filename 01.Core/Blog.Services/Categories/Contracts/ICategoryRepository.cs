using Blog.Entities;
using Services.Categories.Dto;

namespace Services.Categories.Contracts;

public interface ICategoryRepository
{
    Task<Guid> AddCategoryAsync(Category category ,
        CancellationToken  cancellationToken= default);
    
    Task<bool> CategoryExist(string title,
        CancellationToken  cancellationToken = default);
    
    Task<GetCategoryDto?> GetCategoryAsync(Guid categoryId,
        CancellationToken  cancellationToken = default);

}