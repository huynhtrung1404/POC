using Microsoft.AspNetCore.Mvc;
using Poc.App.BusinessServices.Roles.Commands;
using Poc.App.BusinessServices.Roles.Models;
using Poc.App.BusinessServices.Roles.Queries;
using Poc.Mediator;

namespace Poc.Api.Controllers.Managements;

[Authorize]
public class RoleController : BaseApiController
{
    [FromServices]
    public required ISender Sender { get; set; }
    [HttpGet]
    public async Task<IActionResult> GetAllRole()
        => Ok(await Sender.SendAsync(new GetAllRole()));

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDetailRole([FromRoute] Guid id)
         => Ok(await Sender.SendAsync(new GetDetailRole(id)));

    [HttpPost]
    public async Task<IActionResult> AddRole([FromBody] AddRoleDto role)
        => Ok(await Sender.SendAsync(new AddNewRole(role)));

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] RoleDto role)
        => Ok(await Sender.SendAsync(new UpdateRole(role)));

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
        => Ok(await Sender.SendAsync(new DeleteRole(id)));
}