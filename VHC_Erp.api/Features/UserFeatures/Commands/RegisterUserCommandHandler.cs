using Mapster;
using Microsoft.AspNetCore.Identity;
using VHC_Erp.api.Domain.Entities;
using VHC_Erp.api.Infrastructure.Interfaces;
using VHC_Erp.Shared.EntitiesCommands.User;
using VHC_Erp.Shared.SharedLogic;

namespace VHC_Erp.api.Features.UserFeatures.Commands;

public interface IRegisterUserCommandHandler
{
    public Task<Maybe<RegisterUserResponse>> RegisterUserCommandAsync(RegisterUserCommand registerUserCommand);
}

public class RegisterUserCommandHandler(UserManager<UserIdentity> userManager, ITokenService tokenService) : IRegisterUserCommandHandler
{
    public async Task<Maybe<RegisterUserResponse>> RegisterUserCommandAsync(RegisterUserCommand registerUserCommand)
    {
        try
        {
            var newUser = registerUserCommand.Adapt<UserIdentity>();
            return await newUser.ToMaybe()
                .Ensure(async u =>
                {
                    var createResult = await userManager.CreateAsync(newUser, registerUserCommand.Password);
                    return createResult.Succeeded;
                }, "Failed on creating user", 400)
                .Ensure(async u =>
                {
                    var addToRole = await userManager.AddToRoleAsync(newUser, "Admin");
                    return addToRole.Succeeded;
                }, "Error on role assignment", 400)
                .Then(u =>
                {
                    var user = u.Adapt<RegisterUserResponse>() with { Token = tokenService.CreateToken(u) };
                    var userToReturn = user.ToMaybe();
                    return userToReturn;
                },"Error on creating user. Contact a administrator", 500);
        }
        catch (Exception e)
        {
            return Maybe<RegisterUserResponse>.None("Error: " + e.Message, 500);
        }
    }
}