namespace Poc.App.BusinessServices.Authentications;

public class AuthenticationDto
{
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }
    public int ExpireTime { get; set; }
}