using Blog.Entities;
using Services.Categories.Dto;
using Services.Contracts;

namespace Services.Categories.Contracts;

public interface ICategoryService
{
    Task<Result<Guid>> AddAsync(AddCategoryDto dto,
        CancellationToken cancellationToken);

    Task<Result<GetCategoryDto>> GetAsync(Guid id);
    
    Task<Result<Guid>> UpdateAsync(UpdateCategoryDto dto,
        Guid id,
        CancellationToken cancellationToken);

}