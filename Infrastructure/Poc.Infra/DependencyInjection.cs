using Poc.App.Services;
using Poc.Infra.Repositories;
using Poc.Infra.ThirdParties;

namespace Poc.Infra;

public static class DependencyInjection
{
    public static IServiceCollection RegisterInfraService(this IServiceCollection service)
    {
        service.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        service.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        service.AddScoped<IUnitOfWork, UnitOfWork>();
        service.AddScoped<IAwsService, AWSService>();
        service.AddScoped<IAuth0Service, Auth0Service>();
        service.AddHttpClient();
        return service;
    }
}