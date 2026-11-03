namespace Services.Posts.Contracts.Dto;

public class GetPostDto
{
    public Guid Id { get; set; }
    
    public string Title { get; set; } = default!;
    
    public string? ImageUrl { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime? UpdatedAt { get; set; }
    
    public bool IsPublished { get; set; }

    public List<Guid> Categories { get; set; } = [];

    public List<Guid> Tags { get; set; } = [];



}