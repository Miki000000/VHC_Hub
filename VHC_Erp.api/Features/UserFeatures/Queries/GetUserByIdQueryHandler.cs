using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VHC_Erp.api.Domain.Entities;
using VHC_Erp.api.Domain.Entities.UserEntities;
using VHC_Erp.Shared.EntitiesQueries.User;
using VHC_Erp.Shared.SharedLogic;

namespace VHC_Erp.api.Features.UserFeatures.Queries;

public interface IGetUserByIdQueryHandler
{
    Task<Option<GetUserByIdResponse>> GetUserByIdAsync(string id);
}
public class GetUserByIdQueryHandler(UserManager<UserIdentity> userManager) : IGetUserByIdQueryHandler
{
    public async Task<Option<GetUserByIdResponse>> GetUserByIdAsync(string id)
    {
        var user = await userManager.Users.FirstOrDefaultAsync(u => u.Id == id);
        if (user == null) return user!.None<GetUserByIdResponse>("User not found!", 404);
        return user.Some<GetUserByIdResponse>();
    }
}