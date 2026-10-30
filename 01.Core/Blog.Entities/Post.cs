using Blog.Entities.Abstracts;

namespace Blog.Entities;

public class Post :Entity
{
    public string Title { get; set; } = default!;
    
    public string? ImageUrl { get; set; }

    public HashSet<Tag> Tags { get; set; } = [];
    
    public bool IsPublished { get; set; }
    
}