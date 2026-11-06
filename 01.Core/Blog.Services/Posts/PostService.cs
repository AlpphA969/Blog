using System.Runtime.InteropServices.JavaScript;
using Blog.Entities;
using Services.Categories.Contracts;
using Services.Categories.Dto;
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
    private readonly IPostTagRepository  _postTagRepository;
    

    public PostService(IPostRepository postRepository, IPostCategoryRepository postCategoryRepository, IFileService fileService, IUnitOfWork unitOfWork, ICategoryRepository categoryRepository, ITagRepository tagRepository, IPostTagRepository postTagRepository)
    {
        _postRepository = postRepository;
        _postCategoryRepository = postCategoryRepository;
        _fileService = fileService;
        _unitOfWork = unitOfWork;
        _categoryRepository = categoryRepository;
        _tagRepository = tagRepository;
        _postTagRepository = postTagRepository;
    }

    public async Task<Result<AddPostResultDto>> CreatePostAsync(
        AddPostDto dto , 
        CancellationToken cancellationToken = default)
    {

        if (!dto.Categories.Any())
        {
            return PostExceptions.CategoryRequired();
        }

        if (!dto.Tags.Any())
        {
            return PostExceptions.TagRequired();
        }
        
        var tagsValidation =
            await _tagRepository.TagsValidation(dto.Tags,
                cancellationToken);
        if (!tagsValidation)
        {
            return PostExceptions.TagInvalid();
        }
        
        var categoriesValidation =
            await _categoryRepository
                .CategoriesValidation(dto.Categories , cancellationToken);
            
        if (categoriesValidation == false)
        {
            return PostExceptions.CategoryInvalid();

        }
        
        var post = new Post
        {
            Title = dto.Title,
            IsPublished = dto.IsPublished,
        };
        var postId = await _postRepository.AddPostAsync(post, cancellationToken);

        foreach (var i in dto.Categories)
        {
            var postCategory = new PostCategory
            {
                PostId = post.Id,
                CategoryId = i
            };
            await _postCategoryRepository.AddPostCategoryAsync(
                postCategory,
                cancellationToken);
            
        }

        foreach (var id in dto.Tags)
        {
            var postTag = new PostTag
            {
                PostId = post.Id,
                TagId = id
            };
            await _postTagRepository.AddPostTagAsync(
                postTag,
                cancellationToken);
        }
        
        

        
        if(dto.File is not null)
        {
            var result = await _fileService.UploadFileAsync(dto.File);
            if (!result.IsSuccess)
            {
                return result.Error!;
            }
            post.ImageUrl = result.Value;
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

    public async Task<Result<Guid>> UpdateTitleAsync(
        UpdateTitleDto dto,
        Guid id,
        CancellationToken cancellationToken)
    {
        
        var post = await _postRepository.GetPostByIdAsync(id , cancellationToken);
        if (post is null)
        {
            return PostExceptions.PostNotFound();
        }

        post.Title = dto.Title;
        post.UpdatedAt = DateTime.UtcNow;
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return post.Id;
        
    }

    public async Task<Result<Guid>> UpdateFileAsync(
        UpdateFileDto dto,
        Guid id, 
        CancellationToken cancellationToken)
    {
        var post =
            await _postRepository.GetPostByIdAsync(id, cancellationToken);
        if (post is null)
        {
            return PostExceptions.PostNotFound();
        }
        
        var result = await _fileService.UploadFileAsync(dto.File);
        if (!result.IsSuccess)
        {
            return result.Error!;
        }
        post.ImageUrl = result.Value;
        post.UpdatedAt =  DateTime.UtcNow;
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return post.Id;
        
        
    }

    public async Task<Result<Guid>> UpdateCategoryAsync(
        UpdateCategoriesDto dto,
        Guid id,
        CancellationToken cancellationToken)
    {
        if (!dto.Categories.Any())
        {
            return PostExceptions.CategoryRequired();
        }
        var post =
            await _postRepository.GetPostByIdAsync(id, cancellationToken);
        if (post is null)
        {
            return PostExceptions.PostNotFound();
        }
        
        var categoriesExistCheck = await _categoryRepository
            .CategoriesValidation(dto.Categories, cancellationToken);
        if (!categoriesExistCheck)
        {
            return PostExceptions.CategoryInvalid();
        }
            
        var categoriesAlreadyExist = await _postCategoryRepository
            .GetCategoriesAsync(id, cancellationToken);
            
        var validCategories = dto.Categories.Except(categoriesAlreadyExist);

        if (validCategories.Any())
        {
            foreach (var i in validCategories)
            {
                var postCategory = new PostCategory
                {
                    PostId = id,
                    CategoryId = i,
                };

                await _postCategoryRepository.AddPostCategoryAsync(
                    postCategory,
                    cancellationToken);
            }
        }
        post.UpdatedAt = DateTime.UtcNow;
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return post.Id;
        
    }

    public async Task<Result<Guid>> UpdateTagAsync(
        UpdateTagsDto dto,
        Guid id,
        CancellationToken cancellationToken)
    {
        
        if (!dto.TagIds.Any())
        {
            return PostExceptions.TagInvalid();
        }
        var post =
            await _postRepository.GetPostByIdAsync(id, cancellationToken);
        if (post is null)
        {
            return PostExceptions.PostNotFound();
        }

        var tagsValidation =
            await _tagRepository.TagsValidation(dto.TagIds, cancellationToken);
        if (!tagsValidation)
        {
            return PostExceptions.TagInvalid();
        }

        var existTags =
            await _postTagRepository.GetTagsAsync(id, cancellationToken);

        var validTags = dto.TagIds.Except(existTags);

        if (validTags.Any())
        {
            foreach (var t in validTags)
            {
                var postTag = new PostTag
                {
                    PostId = id,
                    TagId = t,
                };
                await _postTagRepository.AddPostTagAsync(postTag,
                    cancellationToken);
                

            }
        }
        post.UpdatedAt = DateTime.UtcNow;
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return post.Id;
        
    }

    public async Task<Result<Guid>> PublishAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        var post = await _postRepository.GetPostByIdAsync(id, cancellationToken);
        if (post is null)
        {
            return PostExceptions.PostNotFound();
        }

        post.IsPublished = true;
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return post.Id;
        
    }
}