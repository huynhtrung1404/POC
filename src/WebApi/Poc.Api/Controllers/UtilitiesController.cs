using Microsoft.AspNetCore.Mvc;
using Poc.App.BusinessServices.Utilities;
using Poc.App.BusinessServices.Utilities.Commands;
using Poc.App.Services;
using Poc.Mediator;

namespace Poc.Api.Controllers;

public class UtilitiesController(IUtilityService utilitiesService, ISender sender) : BaseApiController
{
    private readonly IUtilityService _utilitiesService = utilitiesService;
    private readonly ISender _sender = sender;

    [HttpGet("GetMigrations")]
    public async Task<IActionResult> GetMigrationsAsync()
    {
        var result = await _utilitiesService.GetMigrationLogAsync();
        return Ok(result);
    }

    [HttpGet("GetNewGuid")]
    public IActionResult GetNewGuidAsync(int quantity) => Ok(_utilitiesService.ListGuid(quantity));

    [HttpGet("EncodeBase64")]
    public IActionResult EncodeBase64(string input) => Ok(_utilitiesService.EncodeBase64(input));

    [HttpGet("DecodeBase64")]
    public IActionResult DecodeBase64(string input) => Ok(_utilitiesService.DecodeBase64(input));

    [HttpGet("EncryptData")]
    public IActionResult EncryptData([FromServices] IEncryptService encryptService, [FromQuery] string input)
        => Ok(encryptService.Encrypt(input));

    [HttpGet("DecryptData")]
    public IActionResult DecryptData([FromServices] IEncryptService encryptService, [FromQuery] string input)
        => Ok(encryptService.Decrypt(input));

    [HttpPost("AddM2MAuthentication")]
    public async Task<IActionResult> AddM2MAuthentication([FromBody] AddM2MAuthentication.RequestBody request)
        => Ok(await _sender.SendAsync(new AddM2MAuthentication.Request(request)));

    [HttpGet("generateAzureToken")]
    public async Task<IActionResult> GenerateAzureAccessToken()
        => Ok(await _sender.SendAsync(new GenerateTokenRequest()));
}