using Poc.App.Commons;
using Poc.App.Services;

namespace Poc.Api.Infrastructures;

public class UserService(IHttpContextAccessor httpContextAccessor) : IUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public string UserName => _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type == ConstantClaim.UserName)?.Value!;

    public string Email => _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type == ConstantClaim.Email)?.Value!;

    public string SessionId => _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type == ConstantClaim.Session)?.Value!;

}