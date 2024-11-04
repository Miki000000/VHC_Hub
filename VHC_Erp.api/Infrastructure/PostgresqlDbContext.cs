using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VHC_Erp.api.Domain.Entities;
using VHC_Erp.api.Domain.Entities.UserEntities;
using VHC_Erp.api.Infrastructure.EntitiesConfiguration;
using VHC_Erp.api.Infrastructure.EntitiesConfiguration.UserConfigurations;

namespace VHC_Erp.api.Infrastructure;

public class PostgresqlDbContext(DbContextOptions<PostgresqlDbContext> options)
    : IdentityDbContext<UserIdentity>(options)
{
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<PermissionCategory> PermissionCategories { get; set; }
    public DbSet<UserPermissions> UserPermissions { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfiguration(new UserRolesConfiguration());
        builder.ApplyConfiguration(new PermissionConfigurations());
        builder.ApplyConfiguration(new PermissionCategoryConfigurations());
        builder.ApplyConfiguration(new UserPermissionsConfigurations());
    }
}