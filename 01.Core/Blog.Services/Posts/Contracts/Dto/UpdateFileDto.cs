using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Services.Posts.Contracts.Dto;

public class UpdateFileDto
{
    [Required] public IFormFile File { get; set; }
}