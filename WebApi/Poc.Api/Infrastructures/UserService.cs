using System.Security.Claims;
using Poc.App.Services;

namespace Poc.Api.Infrastructures;

public class UserService(IHttpContextAccessor httpContextAccessor) : IUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public string UserName => _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;

    public string Email => _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value!;

    public string SessionId => _httpContextAccessor.HttpContext?.User.FindFirst("SessionId")?.Value!;
}