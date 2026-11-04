using Blog.Entities;
using Services.Categories.Contracts;
using Services.Contracts;
using Services.FileService.Contracts;
using Services.Posts.Contracts;
using Services.Posts.Contracts.Dto;
using Services.Posts.Contracts.Exceptions;
using Services.Tags.Contracts;
using Servisec;

namespace Services.Posts;

public class PostService : IPostService
{
    
    private readonly IPostRepository _postRepository;
    private readonly IPostCategoryRepository  _postCategoryRepository;
    private readonly IFileService  _fileService;
    private readonly IUnitOfWork  _unitOfWork;
    private readonly ICategoryRepository _categoryRepository;
    private readonly ITagRepository _tagRepository;
    

    public PostService(IPostRepository postRepository, IPostCategoryRepository postCategoryRepository, IFileService fileService, IUnitOfWork unitOfWork, ICategoryRepository categoryRepository, ITagRepository tagRepository)
    {
        _postRepository = postRepository;
        _postCategoryRepository = postCategoryRepository;
        _fileService = fileService;
        _unitOfWork = unitOfWork;
        _categoryRepository = categoryRepository;
        _tagRepository = tagRepository;
    }

    public async Task<Result<AddPostResultDto>> CreatePostAsync(AddPostDto dto , 
        CancellationToken cancellationToken = default)
    {
        if (dto.Categories.Count == 0)
        {
            return PostExceptions.CategoryRequired();
        }

        var categoriesValidation =
            await _categoryRepository
                .CategoriesValidation(dto.Categories , cancellationToken);
        
        if (categoriesValidation == false)
        {
            return PostExceptions.CategoryInvalid();

        }

        if (dto.Tags.Count == 0)
        {
            return PostExceptions.TagInvalid();
        }

        var tagsValidation = await _tagRepository.TagsValidation(
            dto.Tags,
            cancellationToken);
        if (tagsValidation == false)
        {
            return PostExceptions.TagInvalid();
        }
        
        var post = new Post
        {
            Title = dto.Title,
            IsPublished = dto.IsPublished,
        };
        if(dto.File is not null)
        {
            var result = await _fileService.UploadFileAsync(dto.File);
            if (!result.IsSuccess)
            {
                return result.Error!;
            }
            post.ImageUrl = result.Value;
        }
        var postId = await _postRepository.AddPostAsync(post, cancellationToken);
        
        foreach (var categoryId in dto.Categories)
        {
            var postCategory = new PostCategory
            {
                PostId = postId,
                CategoryId = categoryId

            };
            await _postCategoryRepository.AddPostCategoryAsync(
                postCategory,
                cancellationToken);
        }
        var addPostResultDto = new AddPostResultDto()
        {
            Id = postId,
            ImageUrl = post.ImageUrl!,

        };
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return addPostResultDto;


    }

    public async Task<Result<GetPostDto>> GetPostAsync(Guid id,
        CancellationToken cancellationToken)
    {
        var postdto = await _postRepository.GetPostAsync(id, cancellationToken);
        if (postdto is null)
        {
            return PostExceptions.PostNotFound();
        }

        return postdto;

    }
}