using Poc.App.Models;

namespace Poc.App.Services;

public interface IAuth0Service
{
    Task<Auth0Result> GetAccessTokenAsync();
}