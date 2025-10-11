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
builder.Services.AddSwaggerDocument();
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
