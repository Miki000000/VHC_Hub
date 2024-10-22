using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VHC_Erp.api.Domain.Entities.UserEntities;

namespace VHC_Erp.api.Infrastructure.EntitiesConfiguration;

public class UserEntityConfiguration : IEntityTypeConfiguration<UserIdentity>
{
    public void Configure(EntityTypeBuilder<UserIdentity> builder)
    {
        builder
            .HasMany(u => u.UserPermissions)
            .WithOne(up => up.User)
            .HasForeignKey(up => up.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}