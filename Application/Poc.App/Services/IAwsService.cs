using Poc.App.Models;

namespace Poc.App.Services;

public interface IAwsService
{
    Task<IdentityResult> SaveAuthenticationAsync(string? accessToken);
    Task<AccountResult> CreateAwsAccount(IdentityResult identityResult, string targetOU);
}