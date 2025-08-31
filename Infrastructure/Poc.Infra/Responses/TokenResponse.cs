using System.Text.Json.Serialization;

namespace Poc.Infra.Responses;

public class TokenResponse
{
    [JsonPropertyName("access_token")]
    public string? AccessToken { get; set; }
    [JsonPropertyName("expires_in")]
    public int? ExpireIn { get; set; }
    [JsonPropertyName("token_type")]
    public string? TokenType { get; set; }
}