using VHC_Erp.api.Features.UserFeatures.Commands;
using VHC_Erp.api.Features.UserFeatures.Queries;
using VHC_Erp.api.Infrastructure.Interfaces;
using VHC_Erp.api.Infrastructure.Services;

namespace VHC_Erp.api.Configurations;

public static class AddDependencies
{
    public static WebApplicationBuilder AddProjectDependencies(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ITokenService, TokenService>();
        builder.Services.AddScoped<IRegisterUserCommandHandler, RegisterUserCommandHandler>();
        builder.Services.AddScoped<IGetAllUsersQueryHandler, GetAllUsersQueryHandler>();
        builder.Services.AddScoped<IGetUserByIdQueryHandler, GetUserByIdQueryHandler>();
        return builder;
    }
}