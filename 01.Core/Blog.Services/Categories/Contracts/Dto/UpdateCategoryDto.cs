using System.ComponentModel.DataAnnotations;

namespace Services.Categories.Dto;

public class UpdateCategoryDto
{
    [Required] public string Title { get; set; } = default!;

}