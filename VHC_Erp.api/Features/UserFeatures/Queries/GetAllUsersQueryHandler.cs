using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VHC_Erp.api.Domain.Entities;
using VHC_Erp.api.Domain.Entities.UserEntities;
using VHC_Erp.api.Utils;
using VHC_Erp.Shared.EntitiesQueries.User;
using VHC_Erp.Shared.SharedLogic;

namespace VHC_Erp.api.Features.UserFeatures.Queries;

public interface IGetAllUsersQueryHandler
{
    Task<Option<List<GetAllUsersResponse>>> GetAllUsersAsync(GetAllUsersQuery query);
}

public class GetAllUsersQueryHandler(UserManager<UserIdentity> userManager) : IGetAllUsersQueryHandler
{
    public async Task<Option<List<GetAllUsersResponse>>> GetAllUsersAsync(GetAllUsersQuery query)
    {
        try
        {
            var users = query.ApplyFilter &&
                        !string.IsNullOrEmpty(query.FilterBy) &&
                        !string.IsNullOrEmpty(query.FilterWithValue)
                ? userManager.Users.ApplyFilter(query.FilterBy!, query.FilterWithValue!)
                : userManager.Users;
            users = users.ApplySort(query.IsDescending, query.OrderBy);
            users = users.ApplyPagination(query.PageNumber, query.PageSize);
            return (await users.ToListAsync()).Some<List<UserIdentity>, List<GetAllUsersResponse>>();
        }
        catch (Exception e)
        {
           return Option<List<GetAllUsersResponse>>.None("Error: " + e.Message);
        }
    }
}