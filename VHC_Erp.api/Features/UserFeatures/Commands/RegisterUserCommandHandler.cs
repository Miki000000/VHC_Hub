using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VHC_Erp.api.Domain.Entities;
using VHC_Erp.api.Domain.Entities.UserEntities;
using VHC_Erp.api.Infrastructure.Interfaces;
using VHC_Erp.Shared.EntitiesCommands.User;
using VHC_Erp.Shared.SharedLogic;

namespace VHC_Erp.api.Features.UserFeatures.Commands;

public interface IRegisterUserCommandHandler
{
    public Task<Option<RegisterUserResponse>> RegisterUserCommandAsync(RegisterUserCommand registerUserCommand);
}

public class RegisterUserCommandHandler(UserManager<UserIdentity> userManager, ITokenService tokenService) : IRegisterUserCommandHandler
{
    public async Task<Option<RegisterUserResponse>> RegisterUserCommandAsync(RegisterUserCommand registerUserCommand)
    {
        try
        {
            var newUser = registerUserCommand.Adapt<UserIdentity>();
            var createResult = await userManager.CreateAsync(newUser, registerUserCommand.Password);
            if (!createResult.Succeeded)
                return newUser.None<UserIdentity, RegisterUserResponse>(string.Join("\n",createResult.Errors.Select(e => e.Description)));
            var addToRoleResult = await userManager.AddToRoleAsync(newUser, "Admin");
            if (!addToRoleResult.Succeeded)
                return newUser.None<UserIdentity, RegisterUserResponse>(string.Join("\n", addToRoleResult.Errors.Select(e => e.Description)));
            return newUser.Some<UserIdentity, RegisterUserResponse>();
        }
        catch (Exception e)
        {
            return Option<RegisterUserResponse>.None(e.Message);
        }
    }
}