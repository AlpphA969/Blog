using Blog.Entities.Abstracts;

namespace Blog.Entities;

public class PostCategory : Entity
{
    public Guid PostId { get; set; }
    
    public  Post? Post { get; set; }
    
    public Guid CategoryId { get; set; }
    
    public  Category? Category { get; set; }
    
    
    
}