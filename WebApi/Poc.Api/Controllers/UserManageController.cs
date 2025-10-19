using Microsoft.AspNetCore.Mvc;
using Poc.App.BusinessServices.Managements.Users;

namespace Poc.Api.Controllers;

[Authorize]
public class UserController(IUserManageService userManageService) : BaseApiController
{
    private readonly IUserManageService _userManageService = userManageService;

    [HttpGet]
    public async Task<IActionResult> GetAllAsync(int pageSize, int pageNumber) => Ok(await _userManageService.GetListAsync(pageSize, pageNumber));

    [HttpGet("{id}")]
    public async Task<IActionResult> GetItemAsync([FromRoute] Guid id) => Ok(await _userManageService.GetDetailAsync(id));

    [HttpPost]
    public async Task<IActionResult> AddItemAsync([FromBody] UserDto user) => Ok(await _userManageService.AddAsync(user));

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] UserDto user) => Ok(await _userManageService.UpdateAsync(id, user));

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
    {
        await _userManageService.DeleteAsync(id);
        return Ok();
    }
}