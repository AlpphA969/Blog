using Blog.Entities.Abstracts;

namespace Blog.Entities;

public class Tag : Entity
{
    public string Title { get; set; } = default!;
    
    public HashSet<PostTag> PostTags { get; set; } = [];


}