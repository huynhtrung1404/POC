using Microsoft.EntityFrameworkCore;
using Poc.Api.Configurations;
using Poc.Api.Services;
using Poc.App.Options;
using Poc.App.Services;
using Poc.Infra.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<PocContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.Configure<Auth0Config>(builder.Configuration.GetSection("Auth0"));
builder.Services.AddDIService();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
});

builder.Services.Configure<AwsConfigure>(builder.Configuration.GetSection("AwsConfigure"));
builder.Services.AddSwaggerDocument();
builder.Services.AddMemoryCache();
builder.Services.AddSingleton<ICachingService, CatchingService>();

var app = builder.Build();
app.UseOpenApi();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerUi();
}

app.UseHttpsRedirection();
app.UseCors();

app.MapEndpointDisplay();

app.Run();
