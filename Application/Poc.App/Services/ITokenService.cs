using Poc.App.Models;

namespace Poc.App.Services;

public interface ITokenService
{
    Task<IdentityResult> LoginAsync();
}