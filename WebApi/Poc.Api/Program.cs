using Microsoft.EntityFrameworkCore;
using Poc.Api.Services;
using Poc.App.Options;
using Poc.App.Services;
using Poc.Infra.Context;
using Poc.App;
using Poc.Infra;
using Poc.Api.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using NSwag.Generation.Processors.Security;
using NSwag;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<PocContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.Configure<Auth0Config>(builder.Configuration.GetSection("Auth0"));
builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
});

builder.Services.Configure<AwsConfigure>(builder.Configuration.GetSection("AwsConfigure"));
builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection("JwtConfig"));
builder.Services.AddSwaggerDocument(config =>
{
    config.OperationProcessors.Add(new OperationSecurityScopeProcessor("JWT token"));
    config.AddSecurity("JWT token", new OpenApiSecurityScheme
    {
        Type = OpenApiSecuritySchemeType.ApiKey,
        Name = "Authorization",
        Description = "Copy 'Bearer ' + valid JWT token into field",
        In = OpenApiSecurityApiKeyLocation.Header
    });
    config.PostProcess = (document) =>
    {
        document.Info.Version = "v1";
        document.Info.Title = "MyRest-API";
        document.Info.Description = "ASP.NET Core 3.1 MyRest-API";
    };
});
builder.Services.AddMemoryCache();
builder.Services.AddSingleton<ICachingService, CatchingService>();
builder.Services.AddApplicationService();
builder.Services.RegisterInfraService();
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration.GetValue<string>("JwtConfig:Issuer"),
        ValidAudience = builder.Configuration.GetValue<string>("JwtConfig:Audience"),
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("JwtConfig:Key") ?? string.Empty))
    };
});

var app = builder.Build();
app.UseMigration();
app.UseOpenApi();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerUi();
}

app.UseHttpsRedirection();
app.UseCors();

app.MapControllers();

app.Run();
