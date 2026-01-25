using System.Text.Json.Serialization;

namespace Poc.App.BusinessServices.Utilities;

public class M2MDto
{
    [JsonPropertyName("access_token")]
    public string? AccessToken { get; set; }
}