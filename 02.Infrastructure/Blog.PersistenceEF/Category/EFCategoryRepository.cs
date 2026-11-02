using Microsoft.EntityFrameworkCore;
using Services.Categories.Contracts;
using Services.Categories.Dto;

namespace Blog.PersistenceEF.Category;

public class EFCategoryRepository : ICategoryRepository
{
    private readonly DbSet<Entities.Category>  _categories;

    public EFCategoryRepository(EFDataContext dbContext)
    {
        _categories = dbContext.Set<Entities.Category>();
    }

    public async Task<Guid> AddCategoryAsync(Entities.Category category , 
        CancellationToken cancellationToken = default)
    {
        var result = await  _categories.AddAsync(category , cancellationToken);
        return result.Entity.Id;
        
    }

    public async Task<bool> CategoryExistAsync(string title,
        CancellationToken cancellationToken = default)
    {
        var result = await _categories.Where(c=>c.Title == title)
            .AnyAsync(cancellationToken);
        return result;
    }

    public async Task<bool> CategoryExistByIdAsync(Guid categoryId,
        CancellationToken cancellationToken = default)
    {
        var result = await _categories.Where(c => c.Id == categoryId)
            .AnyAsync(cancellationToken);
        return result;
    }

    public async Task<GetCategoryDto?> GetCategoryAsync(Guid categoryId,
        CancellationToken cancellationToken = default)
    {
        var category = await _categories
            .Where(c => c.Id == categoryId)
            .Select(c => new GetCategoryDto
            {
                Id = c.Id,
                Title = c.Title,
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt

            }).FirstOrDefaultAsync(cancellationToken);
        
        return category;
    }

    public void UpdateCategoryAsync(Entities.Category category,
        CancellationToken cancellationToken = default)
    {
        _categories.Update(category);
    }

    public async Task<Entities.Category?> GetCategoryByIdAsync(Guid categoryId,
        CancellationToken cancellationToken = default)
    {
        var category = await _categories.Where(c => c.Id == categoryId)
            .FirstOrDefaultAsync(cancellationToken);
        return category;
    }
}