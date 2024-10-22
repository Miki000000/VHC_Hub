using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VHC_Erp.api.Infrastructure;

namespace VHC_Erp.api.Domain.Entities.UserEntities;

public class UserIdentity : IdentityUser
{
    public int Money { get; set; }
    public virtual IList<UserPermissions> UserPermissions { get; set; } = new List<UserPermissions>();

    public async Task<bool> UserHasPermission(string permissionName, PostgresqlDbContext context)
    {
        var permission = await context.Permissions.FirstOrDefaultAsync(p => p.Name == permissionName);
        if (permission is null) return false;
        var userPermission =
            await context.UserPermissions.FirstOrDefaultAsync(up =>
                up.PermissionId == permission.Id && up.UserId == Id);
        if (userPermission is null) return false;
        return true;
    }
}