using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Poc.App.BusinessServices.AwsOrganizations;

namespace Poc.Api.Controllers;

[Authorize]
public class AwsOrganizationController(IAwsOrganizationService awsOrganizationService) : BaseApiController
{
    private readonly IAwsOrganizationService _awsOrganizationService = awsOrganizationService;

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAllAsync() => Ok(await _awsOrganizationService.GetListAsync());

    [HttpGet("GetItemById")]
    public async Task<IActionResult> GetDetailAsync(Guid id) => Ok(await _awsOrganizationService.GetDetailAsync(id));

    [HttpPost("AddNew")]
    public async Task<IActionResult> AddNewAsync([FromBody] AwsOrgDto awsOrgDto) => Ok(await _awsOrganizationService.AddAsync(awsOrgDto));

    [HttpPut("Update")]
    public async Task<IActionResult> UpdateAsync([FromBody] AwsOrgDto awsOrgDto) => Ok(await _awsOrganizationService.UpdateAsync(awsOrgDto));

    [HttpDelete("Delete")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        await _awsOrganizationService.DeleteAsync(id);
        return Ok();
    }

    [HttpGet("GetListOUFromAWS")]
    public async Task<IActionResult> GetListOUFromAWS() => Ok(await _awsOrganizationService.GetListByAwsPortal());

}