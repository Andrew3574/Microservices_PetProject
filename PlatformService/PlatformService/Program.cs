using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PlatformService.Data;
using PlatformService.Data.Repos;
using PlatformService.Extensions;
using PlatformService.Services;
using PlatformService.Profiles;
using PlatformService.Data.Repos.Interfaces;
using PlatformService.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    if (builder.Environment.IsDevelopment())
    {
        options.UseInMemoryDatabase("InMem");
    }
    else if(builder.Environment.IsProduction())
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString("plsql"));         
    }
    
});
builder.Services.AddHttpClient<ICommandServiceClient,CommandServiceClient>();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddSingleton<IMessageBusClient, MessageBusClient>();
builder.Services.AddScoped<IPlatformRepo,PlatformRepo>();
builder.Services.AddScoped<IPlatformService, PlatformManagementService>();
builder.Services.AddGrpc();

var app = builder.Build();

if (app.Environment.IsProduction())
{    
    app.MigrateDb();
    app.PrepPlatforms();
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.MapGrpcService<GrpcPlatformService>();


app.Run();
