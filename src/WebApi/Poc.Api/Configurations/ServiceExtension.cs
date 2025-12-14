
using Poc.App.Services;
using Poc.Domain.Repositories;
using Poc.Infra.Repositories;
using Poc.Infra.ThirdParties;

namespace Poc.Api.Configurations;

public static class ServiceExtension
{
    public static IServiceCollection AddDIService(this IServiceCollection service)
    {
        service.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        service.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        service.AddScoped<IUnitOfWork, UnitOfWork>();
        service.AddScoped<IAuth0Service, Auth0Service>();
        service.AddScoped<IAwsService, AWSService>();
        return service;
    }
}