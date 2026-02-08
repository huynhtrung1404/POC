using Poc.App.Services;

namespace Poc.Infra.ThirdParties;

public class AwsClientFactory : IAwsClientFactory
{
    public Task<T> CreateClientAsync<T>(string accountId, string region, CancellationToken cancellationToken) where T : class
    {
        throw new NotImplementedException();
    }
}