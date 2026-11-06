using Blog.Entities;
using Services.Contracts;

namespace Services.Posts.Contracts.Exceptions;

public static class PostExceptions
{
    public static Error CategoryRequired() => new Error("CategoryRequired" ,
        ErrorType.InvalidInput , "CategoryRequired");
    public static Error CategoryInvalid() => new Error("CategoryInvalid" ,
        ErrorType.InvalidInput , "CategoryInvalid");
    
    public static Error TagInvalid() => new Error("TagInvalid" ,
        ErrorType.InvalidInput , "TagInvalid");
    
    public static Error PostNotFound() => new Error("PostNotFound" ,
        ErrorType.NotFound , "PostNotFound");
    
    public static Error TagRequired() => new Error("TagRequired" ,
        ErrorType.InvalidInput , "TagRequired");
    
    
    
    
}