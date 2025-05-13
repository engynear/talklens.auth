using Microsoft.AspNetCore.Identity;
using TalkLens.Auth.Core.DTOs.Auth;
using TalkLens.Auth.Core.Entities;
using TalkLens.Auth.Core.Interfaces;

namespace TalkLens.Auth.Core.Services;

public class AuthService(
    IUserRepository userRepository,
    IJwtService jwtService,
    IPasswordHasher<User> passwordHasher)
    : IAuthService
{
    public async Task<AuthResponse> RegisterAsync(string userName, string password)
    {
        var existingUser = await userRepository.GetByUserNameAsync(userName);
        if (existingUser != null)
        {
            return new AuthResponse { Success = false, Error = "User with this username already exists" };
        }

        var user = new User
        {
            UserName = userName,
            CreatedAt = DateTime.UtcNow,
            IsActive = true
        };

        user.PasswordHash = passwordHasher.HashPassword(user, password);

        var created = await userRepository.CreateAsync(user);
        if (!created)
        {
            return new AuthResponse { Success = false, Error = "Failed to create user" };
        }

        var userProfile = await GetUserProfileAsync(user.UserName);

        var token = jwtService.GenerateToken(user);
        return new AuthResponse { Success = true, Token = token, User = userProfile };
    }

    public async Task<AuthResponse> LoginAsync(string userName, string password)
    {
        var user = await userRepository.GetByUserNameAsync(userName);
        if (user == null)
        {
            return new AuthResponse { Success = false, Error = "Invalid username or password" };
        }

        var result = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
        if (result == PasswordVerificationResult.Failed)
        {
            return new AuthResponse { Success = false, Error = "Invalid username or password" };
        }

        user.LastLoginAt = DateTime.UtcNow;
        await userRepository.UpdateAsync(user);

        var token = jwtService.GenerateToken(user);
        var userProfile = await GetUserProfileAsync(user.UserName);
        return new AuthResponse { Success = true, Token = token, User = userProfile};
    }

    public async Task<UserDto?> GetUserProfileAsync(string username)
    {
        var user = await userRepository.GetByUserNameAsync(username);
        if (user == null)
        {
            return null;
        }

        return new UserDto
        {
            Id = user.Id,
            UserName = user.UserName,
            CreatedAt = user.CreatedAt,
            LastLoginAt = user.LastLoginAt,
            IsActive = user.IsActive
        };
    }

    public string? ValidateToken(string token)
    {
        return jwtService.ValidateToken(token);
    }
} 