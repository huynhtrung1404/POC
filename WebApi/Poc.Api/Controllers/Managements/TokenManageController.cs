using Microsoft.AspNetCore.Mvc;
using Poc.App.BusinessServices.Managements.Tokens;

namespace Poc.Api.Controllers.Managements;

[Authorize]
public class TokenManageController(IManageTokenService tokenService) : BaseApiController
{
    private readonly IManageTokenService _tokenService = tokenService;

    [HttpGet]
    public async Task<IActionResult> GetAllAsync(int pageSize, int pageNumber) => Ok(await _tokenService.GetAllAsync(pageSize, pageNumber));

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDetailAsync([FromRoute] Guid id) => Ok(await _tokenService.GetDetailAsync(id));

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid id) => Ok(await _tokenService.DeleteAsync(id));
}