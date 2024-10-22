using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VHC_Erp.api.Domain.Entities.UserEntities;

namespace VHC_Erp.api.Infrastructure.EntitiesConfiguration;

public class PermissionConfigurations : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.HasKey(p => p.Id);
        
        builder
            .HasOne(p => p.PermissionCategory)
            .WithMany(p => p.Permissions)
            .HasForeignKey(p => p.PermissionCategoryId);

        builder
            .HasMany(p => p.UserPermissions)
            .WithOne(up => up.Permission)
            .HasForeignKey(up => up.PermissionId);
    }
}

public class PermissionCategoryConfigurations : IEntityTypeConfiguration<PermissionCategory>
{
    public void Configure(EntityTypeBuilder<PermissionCategory> builder)
    {
        builder.HasKey(p => p.Id);
        builder
            .HasMany(p => p.Permissions)
            .WithOne(p => p.PermissionCategory)
            .HasForeignKey(p => p.PermissionCategoryId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
    
public class UserPermissionsConfigurations : IEntityTypeConfiguration<UserPermissions>
{
    public void Configure(EntityTypeBuilder<UserPermissions> builder)
    {
        builder.HasKey(u => new { u.UserId, u.PermissionId });
        
        builder
            .HasOne(up => up.User)
            .WithMany(u => u.UserPermissions)
            .HasForeignKey(up => up.UserId);
        
        builder
            .HasOne(up => up.Permission)
            .WithMany()
            .HasForeignKey(up => up.PermissionId);
    }
}
