namespace VHC_Erp.Shared.EntitiesQueries.User;

public record GetAllUsersResponse(string Email, string UserName, string Password, int Money);
public record GetAllUsersQuery(bool ApplyFilter, string? FilterBy, string? FilterWithValue, bool IsDescending, string? OrderBy, int PageNumber, int PageSize);