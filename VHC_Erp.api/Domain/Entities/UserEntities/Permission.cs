using System.ComponentModel.DataAnnotations.Schema;

namespace VHC_Erp.api.Domain.Entities.UserEntities;

public class Permission
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int PermissionCategoryId { get; set; }
    public virtual PermissionCategory PermissionCategory { get; set; } = new PermissionCategory();
    public virtual IList<UserPermissions> UserPermissions { get; set; } = new List<UserPermissions>();
}