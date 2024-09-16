using Mapster;
using Microsoft.AspNetCore.Identity;
using VHC_Erp.api.Domain.Entities;
using VHC_Erp.api.Infrastructure.Interfaces;
using VHC_Erp.Shared.EntitiesCommands.User;

namespace VHC_Erp.api.Features.UserFeatures.Commands;

public interface IRegisterUserCommandHandler
{
    public Task<RegisterUserResponse?> RegisterUserCommandAsync(RegisterUserCommand registerUserCommand);
}

public class RegisterUserCommandHandler(UserManager<UserIdentity> userManager, ITokenService tokenService) : IRegisterUserCommandHandler
{
    public async Task<RegisterUserResponse?> RegisterUserCommandAsync(RegisterUserCommand registerUserCommand)
    {
        try
        {
            var newUser = registerUserCommand.Adapt<UserIdentity>();
            var registerUser = await userManager.CreateAsync(newUser, registerUserCommand.Password);
            
            if (!registerUser.Succeeded) return null;
            
            var addToRole = await userManager.AddToRoleAsync(newUser, "Admin");
            
            if (!addToRole.Succeeded) return null;
            
            return newUser.Adapt<RegisterUserResponse>() with { Token = tokenService.CreateToken(newUser)};
        }
        catch (Exception e)
        {
            return null;
        }
    }
}