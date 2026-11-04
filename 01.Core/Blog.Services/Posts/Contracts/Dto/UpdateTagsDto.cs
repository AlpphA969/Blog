using System.ComponentModel.DataAnnotations;

namespace Services.Posts.Contracts.Dto;

public class UpdateTagsDto
{ 
    [Required]
    public List<Guid> TagIds { get; set; } = [];
}