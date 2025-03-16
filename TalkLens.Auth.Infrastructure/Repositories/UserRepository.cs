using LinqToDB;
using TalkLens.Auth.Core.Entities;
using TalkLens.Auth.Core.Interfaces;
using TalkLens.Auth.Infrastructure.Data;
using TalkLens.Auth.Infrastructure.Data.Models;

namespace TalkLens.Auth.Infrastructure.Repositories;

public class UserRepository(ApplicationDbContext context) : IUserRepository
{
    public async Task<User?> GetByIdAsync(string id)
    {
        var userDb = await context.Users.FirstOrDefaultAsync(u => u.Id == id);
        return userDb != null ? MapToUser(userDb) : null;
    }

    public async Task<User?> GetByUserNameAsync(string userName)
    {
        var userDb = await context.Users.FirstOrDefaultAsync(u => u.UserName == userName);
        return userDb != null ? MapToUser(userDb) : null;
    }

    public async Task<bool> CreateAsync(User user)
    {
        try
        {
            var userDb = MapToUserDb(user);
            await context.InsertAsync(userDb);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> UpdateAsync(User user)
    {
        try
        {
            var userDb = MapToUserDb(user);
            await context.UpdateAsync(userDb);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> DeleteAsync(string id)
    {
        try
        {
            await context.Users.DeleteAsync(u => u.Id == id);
            return true;
        }
        catch
        {
            return false;
        }
    }

    private static User MapToUser(UserDb userDb)
    {
        return new User
        {
            Id = userDb.Id,
            UserName = userDb.UserName,
            PasswordHash = userDb.PasswordHash,
            CreatedAt = userDb.CreatedAt,
            LastLoginAt = userDb.LastLoginAt,
            IsActive = userDb.IsActive
        };
    }

    private static UserDb MapToUserDb(User user)
    {
        return new UserDb
        {
            Id = user.Id,
            UserName = user.UserName,
            PasswordHash = user.PasswordHash,
            CreatedAt = user.CreatedAt,
            LastLoginAt = user.LastLoginAt,
            IsActive = user.IsActive
        };
    }
} 