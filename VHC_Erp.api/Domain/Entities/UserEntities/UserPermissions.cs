namespace VHC_Erp.api.Domain.Entities.UserEntities;

public class UserPermissions
{
    public string UserId { get; set; }
    public virtual UserIdentity User { get; set; }
    public int PermissionId { get; set; }
    public virtual Permission Permission { get; set; } = new Permission();
}