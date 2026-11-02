using Services.Contracts;

namespace Services.FileService.Contracts.FileExceptions;

public static  class FileExceptions
{
    public static Error BadFormatFile() => 
        new Error("BadFormatFile",
            ErrorType.InvalidInput ,
            "The file format is incorrect."  );

    public static Error SizeViolation() =>
        new Error("SizeViolations",
            ErrorType.InvalidInput,
            "The file size is incorrect.");

}