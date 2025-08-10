using Poc.App.BusinessServices;
using Poc.App.Features.AwsAccounts;
using Poc.App.Features.AwsOrganizations;
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
        service.AddScoped<ITokenService, TokenService>();
        service.AddScoped<IAwsService, AWSService>();
        service.AddScoped<IAwsOrg, AwsOrg>();
        service.AddScoped<IAwsAccountService, AwsAccountService>();
        return service;
    }
}