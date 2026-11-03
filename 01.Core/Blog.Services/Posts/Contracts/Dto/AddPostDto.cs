using System.ComponentModel.DataAnnotations;
using Blog.Entities;
using Microsoft.AspNetCore.Http;

namespace Services.Posts.Contracts.Dto;

public class AddPostDto
{
    
    
    [Required] public string Title { get; set; } = default!;
    
    public IFormFile? File { get; set; }
    
    public bool IsPublished { get; set; } =  false;

    public HashSet<Guid> Tags { get; set; } = [];
    public HashSet<Guid> Categories { get; set; } = [];
    
    

}