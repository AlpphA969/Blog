using Blog.Entities.Abstracts;

namespace Blog.Entities;

public class Category : Entity
{
    public string Title { get; set; } = default!;

    public HashSet<PostCategory> PostCategories { get; set; } = [];
}