using System.ComponentModel.DataAnnotations;

namespace Services.Posts.Contracts.Dto;

public class UpdateCategoriesDto
{
    [Required] public List<Guid> Categories { get; set; } = [];
}