using System.ComponentModel.DataAnnotations;

namespace Services.Identity.Contracts.Dto;

public class UserDto
{
    [Required] public string Username { get; set; } = default!;
    [Required] public string Password { get; set; } = default!;

}