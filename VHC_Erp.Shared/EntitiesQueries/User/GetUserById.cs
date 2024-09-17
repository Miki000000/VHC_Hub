namespace VHC_Erp.Shared.EntitiesQueries.User;

public record GetUserByIdResponse(string Email, string UserName, string Password, int Money);