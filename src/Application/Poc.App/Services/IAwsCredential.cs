using Poc.App.Models;

namespace Poc.App.Services;

public interface IAwsCredential
{
    Task<CredentialResult> GetOrAssumeRole(string accountId, string roleArn, CancellationToken cancellationToken = default);
}