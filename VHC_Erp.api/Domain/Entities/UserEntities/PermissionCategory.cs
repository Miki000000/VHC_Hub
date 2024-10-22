namespace VHC_Erp.api.Domain.Entities.UserEntities;

public class PermissionCategory
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public virtual IList<Permission> Permissions { get; set; } = new List<Permission>();
}