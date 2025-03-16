using TalkLens.Auth.Core.Entities;

namespace TalkLens.Auth.Core.Interfaces;

public interface IJwtService
{
    string GenerateToken(User user);
    
    string? ValidateToken(string token);
    
    string GetUserIdFromToken(string token);
} 