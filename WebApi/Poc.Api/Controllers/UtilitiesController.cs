using Microsoft.AspNetCore.Mvc;
using Poc.App.BusinessServices.Utilities;

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
}