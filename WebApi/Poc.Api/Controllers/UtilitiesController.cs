using Microsoft.AspNetCore.Mvc;
using Poc.App.BusinessServices.Utilities;
using Poc.App.Services;

namespace Poc.Api.Controllers;

public class UtilitiesController(IUtilityService utilitiesService) : BaseApiController
{
    private readonly IUtilityService _utilitiesService = utilitiesService;

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
}