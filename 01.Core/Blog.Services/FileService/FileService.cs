using Blog.Infrastructure.FileService;
using Microsoft.AspNetCore.Http;
using Services.Contracts;
using Services.FileService.Contracts;
using Services.FileService.Contracts.FileExceptions;

namespace Services.FileService;

public class FileService : IFileService
{
    public async Task<Result<string>> UploadFileAsync(IFormFile file)
    {
        List<string> validExtentions = new List<string>(){".jpg",".txt",".png" , ".pdf"};
        string extension = Path.GetExtension(file.FileName);
        if (!validExtentions.Contains(extension))
        {
            return FileExceptions.BadFormatFile();

        }

        if (file.Length > (5 * 1024 * 1024))
        {
            return FileExceptions.SizeViolation();

        }
        string path = Path.Combine(Directory.GetCurrentDirectory() , "wwwroot/uploads" );
        string fileName = Guid.NewGuid() + extension;
        string fullPath = Path.Combine(path, fileName);
        using FileStream fs = new  FileStream(fullPath, FileMode.Create );
        file.CopyTo(fs);
        return Path.Combine(FileSettings.BaseUrl,fileName);
    }
}