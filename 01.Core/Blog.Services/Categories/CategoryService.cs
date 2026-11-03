using System.Xml;
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
            CategoryExistAsync(dto.Title ,cancellationToken );
        
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

    public async Task<Result<Guid>> UpdateAsync(UpdateCategoryDto dto,
        Guid id,
        CancellationToken cancellationToken)
    {
        var category =
            await _categories.GetCategoryByIdAsync(id, cancellationToken);
        if (category == null)
        {
            return CategoryExceptions.CategoryNotFound();
        }

        var categoryWithTheSameTitleExists = await
            _categories.CategoryExistAsync(dto.Title, cancellationToken);
        if (categoryWithTheSameTitleExists == true)
        {
            return CategoryExceptions.CategoryAlreadyExists();
        }


        category.Title = dto.Title;
        category.UpdatedAt = DateTime.UtcNow;

         await _unitOfWork.SaveChangesAsync(cancellationToken);
         
         return category.Id;

    }
}