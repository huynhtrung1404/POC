namespace Poc.App.BusinessServices.Authentications;

public class LoginDto
{
    public string? UserName { get; set; }
    public string? Password { get; set; }
    public bool IsRemember { get; set; }
}