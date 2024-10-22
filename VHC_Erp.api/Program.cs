using VHC_Erp.api.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.AddApplicationEnvironment()
    .AddProjectDependencies();
builder.Services.AddCors(options =>
{
    options.AddPolicy("VHC_Erp.front", corsBuilder =>
    {
        corsBuilder
            .AllowAnyHeader()
            .WithOrigins("http://localhost:5017")
            .Build();
    });
});

var app = builder.Build();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("VHC_Erp.front");

app.UseHttpsRedirection();
app.UseApplicationEnvironment();

app.Run();