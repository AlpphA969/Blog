using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Services.Posts.Contracts.Dto;

public class UpdateTitleDto
{
    
    [Required] public string Title { get; set; } = default!;
    

}