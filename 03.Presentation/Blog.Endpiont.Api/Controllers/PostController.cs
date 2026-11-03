using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Services.Posts.Contracts;
using Services.Posts.Contracts.Dto;

namespace Blog.Endpiont.Api;
[ApiController]
[Route("api/posts")]
public class PostController
{
    private readonly IPostService  _postService;

    public PostController(IPostService postService)
    {
        _postService = postService;
    }

    [HttpPost]
    public async Task<Result<AddPostResultDto>> AddPost(
        [FromForm] AddPostDto addPostDto ,
        CancellationToken cancellationToken = default)
    {
        var result = await _postService.CreatePostAsync(addPostDto,
            cancellationToken);
        return result;
    }

    [HttpGet("{id}")]
    public async Task<Result<GetPostDto>> GetPostAsync(Guid id,
        CancellationToken cancellationToken = default)
    {
        var dto = await _postService.GetPostAsync(id, cancellationToken);
        return dto;
    }
}