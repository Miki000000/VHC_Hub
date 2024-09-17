using Carter;
using Microsoft.AspNetCore.Http.HttpResults;
using VHC_Erp.api.Features.UserFeatures.Commands;
using VHC_Erp.api.Features.UserFeatures.Queries;
using VHC_Erp.api.Utils;
using VHC_Erp.Shared.EntitiesCommands.User;
using VHC_Erp.Shared.EntitiesQueries.User;

namespace VHC_Erp.api.Endpoints;

public class UserEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var routes = app.MapGroup("api/user");
        routes.MapPost("", RegisterUserEndpoint)
            .Produces<RegisterUserResponse>()
            .Produces(400);
        routes.MapGet("/{id}", GetUserById)
            .Produces<GetUserByIdResponse>()
            .Produces(404);

        routes.MapGet("", GetAllUsers)
            .Produces<List<GetAllUsersResponse>>()
            .Produces(404);
    }

    //Do a request to the service to register user
    async Task<IResult> RegisterUserEndpoint(RegisterUserCommand command, IRegisterUserCommandHandler handler)
    {
        var result = await handler.RegisterUserCommandAsync(command);
        return result.HandleResponse();
    }

    async Task<IResult> GetUserById(string id, IGetUserByIdQueryHandler handler)
    {
        var result = await handler.GetUserByIdAsync(id);
        return result.HandleResponse();
    }

    async Task<IResult> GetAllUsers(bool applyFilter,
        string? filterBy,
        string? filterWithValue,
        bool isDescending,
        string? orderBy,
        int pageNumber,
        int pageSize,
        IGetAllUsersQueryHandler handler)
    {
        var query = new GetAllUsersQuery(applyFilter,
            filterBy,
            filterWithValue,
            isDescending,
            orderBy,
            pageNumber,
            pageSize);
        var result = await handler.GetAllUsersAsync(query);
        return result.HandleResponse();
    }
}