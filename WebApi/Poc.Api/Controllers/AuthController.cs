using Microsoft.AspNetCore.Mvc;
using Poc.App.BusinessServices.Authentications;

namespace Poc.Api.Controllers;

public class AuthController : BaseApiController
{
    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginDto login, [FromServices] IAuthenticationService authenticationService)
    {
        return Ok(await authenticationService.LoginAsync(login));
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterDto register, [FromServices] IAuthenticationService authenticationService)
    {
        await authenticationService.RegisterAsync(register);
        return Ok();
    }
}