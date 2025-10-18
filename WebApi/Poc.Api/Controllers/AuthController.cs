using Microsoft.AspNetCore.Mvc;
using Poc.App.BusinessServices.Authentications;

namespace Poc.Api.Controllers;

public class AuthController(IAuthenticationService authenticationService) : BaseApiController
{
    private readonly IAuthenticationService _authenticationService = authenticationService;

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginDto login) =>
         Ok(await _authenticationService.LoginAsync(login));

    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterDto register)
    {
        await _authenticationService.RegisterAsync(register);
        return Ok();
    }

    [HttpPost("refreshToken")]
    public async Task<IActionResult> RefreshTokenAsync([FromBody] RefreshTokenDto model) =>
        Ok(await _authenticationService.RefreshTokenAsync(model));

    [HttpGet("userInfo")]
    public IActionResult GetUserInfo() =>
        Ok(_authenticationService.GetUserInfo());

}