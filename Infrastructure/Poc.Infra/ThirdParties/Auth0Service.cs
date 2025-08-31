using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Poc.App.Models;
using Poc.App.Options;
using Poc.App.Services;
using Poc.Infra.Responses;

namespace Poc.Infra.ThirdParties;

public class Auth0Service(IOptionsSnapshot<Auth0Config> option, ILogger<Auth0Service> logger) : IAuth0Service
{
    private readonly Auth0Config _auth0Config = option.Value;
    private readonly ILogger<Auth0Service> _logger = logger;

    public async Task<Auth0Result> GetAccessTokenAsync()
    {
        using var httpClient = new HttpClient();

        var tokenEndpoint = $"https://{_auth0Config?.Domain ?? string.Empty}/oauth/token";
        _logger.LogInformation("Endpoint for using data {tokenEndpoint}", tokenEndpoint);
        var requestBody = new
        {
            client_id = _auth0Config?.ClientId ?? string.Empty,
            client_secret = _auth0Config?.ClientSecret ?? string.Empty,
            audience = _auth0Config?.Audience ?? string.Empty,
            grant_type = "client_credentials"
        };

        var content = new StringContent(
            JsonSerializer.Serialize(requestBody),
            System.Text.Encoding.UTF8,
            "application/json"
        );

        var response = await httpClient.PostAsync(tokenEndpoint, content);
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        _logger.LogInformation("Result get from auth0 is {result}", responseContent);
        var tokenResponse = JsonSerializer.Deserialize<TokenResponse>(responseContent);

        return new Auth0Result
        {
            AccessToken = tokenResponse?.AccessToken,
            ExpireTime = DateTime.UtcNow.AddSeconds(tokenResponse?.ExpireIn ?? 0),
            TokenType = tokenResponse?.TokenType
        };
    }
}