using Microsoft.AspNetCore.Mvc;
using Poc.App.BusinessServices.Managements.AuthConfigs;

namespace Poc.Api.Controllers.Managements;

[Authorize]

public class AuthConfigController(IAuthConfigService authConfigService) : BaseApiController
{
    private readonly IAuthConfigService _authConfigService = authConfigService;

    [HttpGet("getAll")]
    public async Task<IActionResult> GetAllAsync(int pageSize, int pageNumber)
    {
        return Ok(await _authConfigService.GetAllAsync(pageSize, pageNumber));
    }

    [HttpGet("getDetail/{id}")]
    public async Task<IActionResult> GetDetailAsync([FromRoute] Guid id)
    {
        return Ok(await _authConfigService.GetDetailItem(id));
    }

    [HttpPost("addNew")]
    public async Task<IActionResult> AddNewAsync([FromBody] AuthConfigDto config)
    {
        return Ok(await _authConfigService.AddAsync(config));
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateAsync([FromBody] AuthConfigDto config)
    {
        return Ok(await _authConfigService.UpdateAsync(config));
    }

    [HttpDelete("remove/{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
    {
        return Ok(await _authConfigService.DeleteAsync(id));
    }

}