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
}