using System.ComponentModel.DataAnnotations;

namespace TalkLens.Auth.Core.DTOs.Auth;

public class RegisterRequest
{
    [Required]
    [StringLength(50)]
    public string UserName { get; set; } = string.Empty;

    [Required]
    [StringLength(100, MinimumLength = 6)]
    public string Password { get; set; } = string.Empty;
} 