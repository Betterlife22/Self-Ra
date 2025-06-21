using Amazon.S3;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SELF_RA.DI;
using SELF_RA.Hubs;
using SELF_RA.Middleware;
using SELF_RA.Middleware.ChatFPT.API.Middleware;
using Selfra_Core.Base;
using Selfra_Entity;
using Selfra_Entity.Model;
using Selfra_Repositories.Base;
using Selfra_Services;
using Selfra_Services.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();
builder.Services.AddApplication(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddSwaggerGen();
builder.Services.AddDefaultAWSOptions(builder.Configuration.GetAWSOptions());
builder.Services.AddAWSService<IAmazonS3>();
builder.Services.AddSignalR();
builder.Services.AddSingleton<IUserIdProvider, CustomUserIdProvider>();
builder.Services.AddHttpClient();
builder.Services.Configure<JwtSettings>(
    builder.Configuration.GetSection("JwtSettings"));

builder.Services.AddSingleton(sp =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    var settings = new JwtSettings();
    config.GetSection("JwtSettings").Bind(settings);
    settings.IsValid(); 
    return settings;
});
builder.Services.AddHttpClient<GPTClassificationService>();

var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseDeveloperExceptionPage();
}
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "SelfRaApp API V1");
    c.RoutePrefix = "swagger";
});

app.UseHttpsRedirection();
app.UseCors("CorsPolicy");
 app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<PermissionMiddleware>();
app.MapControllers();
app.MapHub<ChatHub>("/chathub", opt =>
{
    opt.Transports = Microsoft.AspNetCore.Http.Connections.HttpTransportType.WebSockets;
});
app.Run();
