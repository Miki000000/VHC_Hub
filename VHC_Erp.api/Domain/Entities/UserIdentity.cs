using Microsoft.AspNetCore.Identity;

namespace VHC_Erp.api.Domain.Entities;

public class UserIdentity : IdentityUser
{
    public int Money { get; set; }
}