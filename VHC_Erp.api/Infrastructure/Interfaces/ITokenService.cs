using VHC_Erp.api.Domain.Entities;

namespace VHC_Erp.api.Infrastructure.Interfaces;

public interface ITokenService
{
    string CreateToken(UserIdentity users);
}