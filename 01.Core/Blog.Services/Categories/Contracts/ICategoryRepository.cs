using Blog.Entities;
using Services.Categories.Dto;

namespace Services.Categories.Contracts;

public interface ICategoryRepository
{
    Task<Guid> AddCategoryAsync(Category category ,
        CancellationToken  cancellationToken= default);
    
    Task<bool> CategoryExistAsync(string title,
        CancellationToken  cancellationToken = default);
    
    Task<GetCategoryDto?> GetCategoryAsync(Guid categoryId,
        CancellationToken  cancellationToken = default);
    
    void UpdateCategoryAsync(Category category,
        CancellationToken cancellationToken = default);

    Task<Category?> GetCategoryByIdAsync(Guid categoryId,
        CancellationToken cancellationToken = default);

}