
using Poc.Domain.Entities;
using Poc.Domain.Repositories;

namespace Poc.App.BusinessServices.Authentications;

public class AuthenticationService(IRepository<Token> tokenRepository, IUnitOfWork unitOfWork) : IAuthenticationService
{
    private readonly IRepository<Token> _tokenRepository = tokenRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public Task<AuthenticationDto> LoginAsync(LoginDto login)
    {
        throw new NotImplementedException();
    }

    public Task LogoutAsync()
    {
        throw new NotImplementedException();
    }

    public Task RegisterAsync()
    {
        throw new NotImplementedException();
    }
}