using VHC_Erp.api.Domain.Entities;
using VHC_Erp.api.Domain.Entities.UserEntities;

namespace VHC_Erp.api.Infrastructure.Interfaces;

public interface ITokenService
{
    string CreateToken(UserIdentity users);
}