using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VHC_Erp.api.Domain.Entities;
using VHC_Erp.Shared.EntitiesQueries.User;
using VHC_Erp.Shared.SharedLogic;

namespace VHC_Erp.api.Features.UserFeatures.Queries;

public interface IGetUserByIdQueryHandler
{
    Task<Maybe<GetUserByIdResponse>> GetUserByIdAsync(string id);
}
public class GetUserByIdQueryHandler(UserManager<UserIdentity> userManager) : IGetUserByIdQueryHandler
{
    public async Task<Maybe<GetUserByIdResponse>> GetUserByIdAsync(string id)
    {
        return (await userManager.Users.FirstOrDefaultAsync(u => u.Id == id))
            .ToMaybe()
            .Then(u => u.Adapt<GetUserByIdResponse>().ToMaybe(), 
                "Not possible converting the type to return", 400);
    }
}