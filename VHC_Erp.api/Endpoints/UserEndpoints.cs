using Carter;
using Microsoft.AspNetCore.Http.HttpResults;
using VHC_Erp.api.Features.UserFeatures.Commands;
using VHC_Erp.Shared.EntitiesCommands.User;

namespace VHC_Erp.api.Endpoints;

public class UserEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var routes = app.MapGroup("api/user");
        
        routes.MapPost("", RegisterUserEndpoint)
        .Produces<RegisterUserResponse>()
        .Produces(404);
    }

    //Do a request to the service to register user
    async Task<IResult> RegisterUserEndpoint(RegisterUserCommand command, IRegisterUserCommandHandler handler)
    {
        var result = await handler.RegisterUserCommandAsync(command);
        if (result is null) return Results.NotFound(result);
        return Results.Ok(result);
    }
}