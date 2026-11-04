using System.ComponentModel.DataAnnotations;
using Blog.Entities;
using Microsoft.AspNetCore.Http;

namespace Services.Posts.Contracts.Dto;

public class AddPostDto
{
    
    
    [Required] public string Title { get; set; } = default!;
    
    public IFormFile? File { get; set; }
    
    public bool IsPublished { get; set; } =  false;

    public List<Guid> Tags { get; set; } = [];
    public List<Guid> Categories { get; set; } = [];
    
    

}