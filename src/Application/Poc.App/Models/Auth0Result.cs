namespace Poc.App.Models;

public class Auth0Result
{
    public string? AccessToken { get; set; }
    public DateTime? ExpireTime { get; set; }
    public string? TokenType { get; set; }
}