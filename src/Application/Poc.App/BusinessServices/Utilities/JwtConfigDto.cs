namespace Poc.App.BusinessServices.Utilities;

public class JwtConfigDto
{
    public string? Issuer { get; set; }
    public string? Audience { get; set; }
    public string? Authority { get; set; }
    public string? SecureMethod { get; set; }
    public string? Data { get; set; }
}