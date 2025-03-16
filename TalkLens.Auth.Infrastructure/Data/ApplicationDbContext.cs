using LinqToDB;
using LinqToDB.Data;
using LinqToDB.Mapping;
using Microsoft.Extensions.Configuration;
using TalkLens.Auth.Infrastructure.Data.Models;

namespace TalkLens.Auth.Infrastructure.Data;

public class ApplicationDbContext : DataConnection
{
    public ApplicationDbContext(IConfiguration configuration) 
        : base(ProviderName.PostgreSQL, configuration.GetConnectionString("DefaultConnection"))
    {
        MappingSchema.SetConvertExpression<DateTime, DateTime>(dt => dt);
    }
    
    public ITable<UserDb> Users => this.GetTable<UserDb>();
} 