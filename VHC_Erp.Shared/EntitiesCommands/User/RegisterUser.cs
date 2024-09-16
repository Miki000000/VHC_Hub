namespace VHC_Erp.Shared.EntitiesCommands.User;

public record RegisterUserCommand(string Email, string UserName, string Password, int Money);
public record RegisterUserResponse(string Email, string UserName, string Password, int Money, string Token);
