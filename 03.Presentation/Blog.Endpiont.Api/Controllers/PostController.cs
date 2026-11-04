using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Matching;
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

    [HttpPut("updatetitle/{id}")]
    public async Task<Result<Guid>> UpdatePostAsync(
        UpdateTitleDto updatePostDto,Guid id,
        CancellationToken cancellationToken = default)
    {
        var result = await _postService.UpdateTitleAsync(
            updatePostDto
            ,id,
            cancellationToken);
        return result;
    }

    [HttpPut("updatefile/{id}")]
    public async Task<Result<Guid>> UpdateFileAsync(
        [FromForm] UpdateFileDto updateFileDto,
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var result = await _postService.UpdateFileAsync(
            updateFileDto,
            id,
            cancellationToken);
        return result;
    }

    [HttpPut("updatecategories/{id}")]
    public async Task<Result<Guid>> UpdateCategoriesAsync(
        UpdateCategoriesDto dto ,Guid id,
        CancellationToken  cancellationToken =default)
    {
        var result = await 
            _postService.UpdateCategoryAsync(dto,id, cancellationToken);
        return result;

    }

    [HttpPut("updatetags/{id}")]
    public async Task<Result<Guid>> UpdateTagsAsync(
        UpdateTagsDto dto,
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var result = await _postService.UpdateTagAsync(dto, id, cancellationToken);
        return result;

    }

    [HttpPut("publish/{id}")]
    public async Task<Result> PublishPostAsync(Guid id,
        CancellationToken cancellationToken = default)
    {
        var result = await _postService.PublishAsync(id ,cancellationToken);
        return result;
    }


}