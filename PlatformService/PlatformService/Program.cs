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
    options.UseInMemoryDatabase("InMem");
    //options.UseNpgsql(builder.Configuration.GetConnectionString("plsql")); 
});
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IPlatformRepo,PlatformRepo>();


builder.Services.AddScoped<IPlatformService, PlatformManagementService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.PrepPlatforms();
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
