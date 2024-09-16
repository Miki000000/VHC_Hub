using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VHC_Erp.api.Domain.Entities;
using VHC_Erp.api.Infrastructure.EntitiesConfiguration;

namespace VHC_Erp.api.Infrastructure;

public class PostgresqlDbContext : IdentityDbContext<UserIdentity>
{
    public PostgresqlDbContext(DbContextOptions<PostgresqlDbContext> options): base(options){}
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfiguration(new UserRolesConfiguration());
    }
}