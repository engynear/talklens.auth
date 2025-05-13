namespace TalkLens.Auth.Core.DTOs.Token;

public class ValidateTokenResponse
{
    public bool IsValid { get; set; }
    public string? UserId { get; set; }
    public string? Message { get; set; }
} 