using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VHC_Erp.api.Domain.Entities.UserEntities;

namespace VHC_Erp.api.Infrastructure.EntitiesConfiguration.UserConfigurations;

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