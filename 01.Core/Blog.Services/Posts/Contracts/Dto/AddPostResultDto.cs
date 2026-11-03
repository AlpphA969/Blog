namespace Services.Posts.Contracts.Dto;

public class AddPostResultDto
{
    public Guid Id { get; set; }

    public string ImageUrl { get; set; } = default!;

}