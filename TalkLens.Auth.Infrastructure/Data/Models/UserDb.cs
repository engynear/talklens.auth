using LinqToDB.Mapping;

namespace TalkLens.Auth.Infrastructure.Data.Models;

[Table("Users")]
public class UserDb
{
    [PrimaryKey]
    public string Id { get; set; } = string.Empty;

    [Column]
    public string UserName { get; set; } = string.Empty;

    [Column]
    public string PasswordHash { get; set; } = string.Empty;

    [Column]
    public DateTime CreatedAt { get; set; }

    [Column]
    public DateTime? LastLoginAt { get; set; }

    [Column]
    public bool IsActive { get; set; }
} 