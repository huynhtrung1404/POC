using Microsoft.AspNetCore.Mvc;
using Poc.App.BusinessServices.AwsOrganizations;

namespace Poc.Api.Controllers;

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

}