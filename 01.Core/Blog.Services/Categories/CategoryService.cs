using Blog.Entities;
using Services.Categories.Contracts;
using Services.Categories.Contracts.Exceptions;
using Services.Categories.Dto;
using Services.Contracts;
using Servisec;

namespace Services.Categories;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categories;
    private readonly IUnitOfWork _unitOfWork;

    public CategoryService(ICategoryRepository categories, IUnitOfWork unitOfWork)
    {
        _categories = categories;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> AddAsync(AddCategoryDto dto,
        CancellationToken cancellationToken)
    {
        var categoryExists = await _categories.
            CategoryExist(dto.Title ,cancellationToken );
        
        if (categoryExists == true)
        {
            return CategoryExceptions.CategoryAlreadyExists();
            
        }

        Category category = new Category
        {
            
            Title = dto.Title,
        };

        var result = await _categories
            .AddCategoryAsync(category, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return result;
    }

    public async Task<Result<GetCategoryDto>> GetAsync(Guid id)
    {
        
        var result = await _categories.GetCategoryAsync(id);
        
        if (result == null)
        {
            return CategoryExceptions.CategoryNotFound();

        }
        return result;

    }
}