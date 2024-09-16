using Carter;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VHC_Erp.api.Domain.Entities;
using VHC_Erp.api.Infrastructure;

namespace VHC_Erp.api.Configurations;

public static class ApplicationExtensions
{
    public static WebApplicationBuilder AddApplicationEnvironment(this WebApplicationBuilder builder)
    {
        builder.Services.AddCarter();
        builder.Services.AddDbContext<PostgresqlDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSqlConnection"))
        );
        builder.Services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler =
                System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        });
        builder.Services.AddIdentity<UserIdentity, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 12;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
            })
            .AddEntityFrameworkStores<PostgresqlDbContext>();  
        return builder;
    }

    public static WebApplication UseApplicationEnvironment(this WebApplication app)
    {
        app.MapCarter();
        return app;
    }
}