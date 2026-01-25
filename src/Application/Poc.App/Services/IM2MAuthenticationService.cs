using Poc.Domain.Enums;

namespace Poc.App.Services;

public interface IM2MAuthenticationService
{
    Task<string> GetAccessToken(LoginType type);
}