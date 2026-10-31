using Services.Contracts;

namespace Services.Identity.Contracts.Exception;

public static class UserExceptions
{
    public static Error UserNotFound()=>
        new Error("user not found", ErrorType.NotFound, "user not found"); 
    
    public static Error WrongPassword()=>
        new Error("Password Is Incorrect", ErrorType.InvalidInput, "Wrong password"); 
    
    
    
}