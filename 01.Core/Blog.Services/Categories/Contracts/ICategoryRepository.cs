using Services.Categories.Dto;

namespace Services.Categories.Contracts;

public interface ICategoryRepository
{
    Task<Guid> AddCategory(AddCategoryDto dto);

}