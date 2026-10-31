using Blog.Entities.Abstracts;

namespace Blog.Entities;

public class Post :Entity
{
    public string Title { get; set; } = default!;
    
    public string? ImageUrl { get; set; }
    
    
    public bool IsPublished { get; set; }
    
    public HashSet<PostCategory> PostCategories { get; set; } = [];
    
    
}