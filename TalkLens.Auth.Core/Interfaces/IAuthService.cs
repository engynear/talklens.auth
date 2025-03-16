using TalkLens.Auth.Core.DTOs.Auth;

namespace TalkLens.Auth.Core.Interfaces;

public interface IAuthService
{
    Task<AuthResponse> RegisterAsync(string userName, string password);
    Task<AuthResponse> LoginAsync(string userName, string password);
    Task<UserDto?> GetUserProfileAsync(string userId);
    string? ValidateToken(string token);
} 