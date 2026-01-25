using System.Net.Mime;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Poc.App.BusinessServices.Utilities;
using Poc.App.Services;
using Poc.Domain.Enums;

namespace Poc.Infra.ThirdParties;

public class M2MAuthenticationService(IHttpClientFactory httpClientFactory, IRepository<M2MAuthentication> m2mRepository, ILogger<M2MAuthenticationService> logger) : IM2MAuthenticationService
{
    private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
    private readonly IRepository<M2MAuthentication> _m2mRepository = m2mRepository;
    private readonly ILogger<M2MAuthenticationService> _logger = logger;

    public async Task<string> GetAccessToken(LoginType loginType)
    {
        var data = await _m2mRepository.GetItemAsync(new M2MAuthenticationSpecification(loginType))
            ?? throw new ArgumentException(loginType.ToString());

        var client = _httpClientFactory.CreateClient();

        var tokenEndpoint = $"https://{data.Url}";
        Console.WriteLine("token endpoint is {0}", tokenEndpoint);
        _logger.LogInformation("Endpoint for using data {tokenEndpoint}", tokenEndpoint);
        var requestBody = new Dictionary<string, string>
        {
            {"client_id" , data.ClientId!},
            {"client_secret" , data.ClientSecret!},
            {"scope" , data.Scope!},
            {"grant_type" , GrantType.client_credentials.ToString()}
        };

        using var content = new FormUrlEncodedContent(requestBody);

        var response = await client.PostAsync(tokenEndpoint, content);
        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError(response.ReasonPhrase);
            _logger.LogError(response.StatusCode.ToString());
            throw new Exception("Request failure");
        }

        var responseContent = await response.Content.ReadAsStringAsync();
        _logger.LogInformation("Success data");

        var result = JsonSerializer.Deserialize<M2MDto>(responseContent);

        return result?.AccessToken!;

    }
}