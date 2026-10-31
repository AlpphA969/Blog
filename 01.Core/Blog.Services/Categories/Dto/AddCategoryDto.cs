using System.ComponentModel.DataAnnotations;

namespace Services.Categories.Dto;

public class AddCategoryDto
{
    [Required] 
    public string Title { get; set; } = default!;

}