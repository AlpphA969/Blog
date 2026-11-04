using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Services.Posts.Contracts.Dto;

public class UpdatePostDto
{
    
    
    [Required] public string Title { get; set; } = default!;
    
    
    public bool IsPublished { get; set; }
    
    
    public IFormFile? File { get; set; }
    
    
    
    
    



}