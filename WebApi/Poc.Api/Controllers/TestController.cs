using Microsoft.AspNetCore.Mvc;
using Poc.Api.Configurations;
using Poc.App.Services;

namespace Poc.Api.Controllers;

public class TestController(IAuth0Service auth0Service) : BaseApiController
{
    private readonly IAuth0Service _auth0Service = auth0Service;

    [HttpGet]
    public async Task<IActionResult> GetAsync() => Ok(await _auth0Service.GetAccessTokenAsync());

}