namespace Poc.App.BusinessServices.Authentications;

public interface IAuthenticationService
{
    Task<AuthenticationDto> LoginAsync(LoginDto login);
    Task LogoutAsync();
    Task RegisterAsync(RegisterDto register);
}