using Poc.App.Models;

namespace Poc.App.Services;

public interface IAwsService
{
    Task<IdentityResult> SaveAuthenticationAsync(string? accessToken);
    Task<IdentityResult> AssumeRoleAws(IdentityResult identityResult, string role, string type);
    Task<AccountResult> CreateAwsAccount(IdentityResult identityResult, string targetOU);
    Task<IEnumerable<(string orgId, string orgName)>> ListOUAsync(IdentityResult identityResult);
}