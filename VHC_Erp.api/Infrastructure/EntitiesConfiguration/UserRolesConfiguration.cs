using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace VHC_Erp.api.Infrastructure.EntitiesConfiguration;

public class UserRolesConfiguration : IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        var roles = new List<IdentityRole>()
        {
            new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" },
            new IdentityRole { Name = "Vendedor", NormalizedName = "VENDEDOR" },
            new IdentityRole { Name = "Financeiro", NormalizedName = "FINANCEIRO" },
        };

        builder.HasData(roles);
    }
}