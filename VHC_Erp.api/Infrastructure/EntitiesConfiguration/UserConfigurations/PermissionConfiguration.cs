using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VHC_Erp.api.Domain.Entities.UserEntities;

namespace VHC_Erp.api.Infrastructure.EntitiesConfiguration.UserConfigurations;

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