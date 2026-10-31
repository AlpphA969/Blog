using Services.Categories.Contracts;
using Services.Categories.Dto;

namespace Blog.PersistenceEF.Category;

public class EFCategoryRepository : ICategoryRepository
{
    public Task<Guid> AddCategory(AddCategoryDto dto)
    {
       // throw new NotImplementedException();
    }
}