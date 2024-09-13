using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace VHC_Erp.api.Infrastructure;

public class PostgresqlDbContext : IdentityDbContext
{
    public PostgresqlDbContext(DbContextOptions<PostgresqlDbContext> options): base(options){}
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}