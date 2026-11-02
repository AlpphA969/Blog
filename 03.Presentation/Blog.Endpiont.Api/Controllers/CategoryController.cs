using Microsoft.AspNetCore.Mvc;
using Services.Categories.Contracts;
using Services.Categories.Dto;
using Services.Contracts;

namespace Blog.Endpiont.Api;
[ApiController]
[Route("api/categories")]
public class CategoryController
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpPost]
    public async Task<Result<Guid>> AddAsync([FromBody] AddCategoryDto dto,
        CancellationToken cancellationToken = default)
    {
        var result = await _categoryService.AddAsync(dto , cancellationToken);
        return result;
    }

    [HttpGet("{id}")]
    public async Task<Result<GetCategoryDto>> GetAsync(Guid id,
        CancellationToken cancellationToken = default)
    {
        var result = await _categoryService.GetAsync(id);
        return result;
    }
}