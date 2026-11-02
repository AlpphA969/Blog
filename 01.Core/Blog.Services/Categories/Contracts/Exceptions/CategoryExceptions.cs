using Services.Contracts;

namespace Services.Categories.Contracts.Exceptions;

public static class CategoryExceptions
{
    public static Error CategoryAlreadyExists()=>
        new Error("Category Already Exist" 
            , ErrorType.InvalidInput,
            "Category Already Exist");
    
    public static Error CategoryNotFound()=>
        new Error("Category Not Found" ,
            ErrorType.NotFound , 
            "Category Not Found");
    
}