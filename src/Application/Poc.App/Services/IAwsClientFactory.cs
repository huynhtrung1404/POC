using Poc.App.Models;

namespace Poc.App.Services;

public interface IAwsClientFactory
{
    Task<T> CreateClientAsync<T>(string accountId, string region, CancellationToken cancellationToken) where T : class;
}