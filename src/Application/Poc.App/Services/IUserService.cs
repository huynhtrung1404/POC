namespace Poc.App.Services;

public interface IUserService
{
    string UserName { get; }
    string Email { get; }
    string SessionId { get; }
}