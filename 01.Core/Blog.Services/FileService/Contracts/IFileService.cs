using Microsoft.AspNetCore.Http;
using Services.Contracts;

namespace Services.FileService.Contracts;

public interface IFileService
{
    Task<Result<string>> UploadFileAsync(IFormFile file);
}