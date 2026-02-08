using Amazon.Runtime;
using Amazon.SecurityToken;
using Microsoft.Extensions.Caching.Memory;
using Poc.App.Models;

namespace Poc.Infra.Commons;

public class AwsCredentialCache(IAmazonSecurityTokenService amazonSTS,
    IMemoryCache memoryCache,
    SemaphoreSlim semaphoreSlim)
{
    private readonly IAmazonSecurityTokenService _amazonSts = amazonSTS;
    private readonly IMemoryCache _memoryCache = memoryCache;
    private readonly SemaphoreSlim _semaphoreSlim = semaphoreSlim;
    private const string SessionRole = "AwsSessionRole";

    public async Task<CredentialResult> GetOrAssumeRole(string accountId, string roleArn, CancellationToken cancellationToken = default)
    {
        var cacheKey = $"sts:{accountId}";

        if (_memoryCache.TryGetValue<CredentialResult>(cacheKey, out var cachedCreds))
            return cachedCreds!;
        await _semaphoreSlim.WaitAsync(cancellationToken);
        try
        {
            if (_memoryCache.TryGetValue(cacheKey, out cachedCreds))
                return cachedCreds!;

            var response = await _amazonSts.AssumeRoleAsync(new()
            {
                RoleArn = roleArn,
                RoleSessionName = $"{SessionRole}-{accountId}",
                DurationSeconds = 3600  //seconds
            }, cancellationToken);

            _memoryCache.Set(cacheKey, cachedCreds);
            var cred = response.Credentials;
            var sessionCreds = new SessionAWSCredentials(
                cred.AccessKeyId,
                cred.SecretAccessKey,
                cred.SessionToken
            );

            var result = new CredentialResult()
            {
                AccessKeyId = cred.AccessKeyId,
                AccountId = accountId,
                Expiration = cred.Expiration ?? DateTime.MinValue,
                SecretAccessKey = cred.SecretAccessKey,
                SessionToken = cred.SessionToken
            };

            _memoryCache.Set(cacheKey, result, TimeSpan.FromMinutes(55));
            return result;
        }
        finally
        {
            _semaphoreSlim.Release();
        }
    }
}