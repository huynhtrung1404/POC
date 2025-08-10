using Auth0.AuthenticationApi;
using Auth0.AuthenticationApi.Models;
using Microsoft.Extensions.Options;
using Poc.App.Models;
using Poc.App.Options;
using Poc.App.Services;

namespace Poc.Infra.ThirdParties;

public class Auth0Service(IOptionsSnapshot<Auth0Config> option) : IAuth0Service
{
    private readonly Auth0Config _auth0Config = option.Value;

    public async Task<Auth0Result> GetAccessTokenAsync()
    {
        var client = new AuthenticationApiClient(_auth0Config?.Domain ?? string.Empty);

        var tokenRequest = new ClientCredentialsTokenRequest
        {
            ClientId = _auth0Config?.ClientId ?? string.Empty,
            ClientSecret = _auth0Config?.ClientSecret ?? string.Empty,
            Audience = _auth0Config?.Audience ?? string.Empty

        };

        var response = await client.GetTokenAsync(tokenRequest);
        return new()
        {
            AccessToken = response.AccessToken,
            ExpireTime = DateTime.UtcNow.AddSeconds(response.ExpiresIn),
            IdToken = response.IdToken,
            RefreshToken = response.RefreshToken,
            TokenType = response.TokenType
        };
    }
}