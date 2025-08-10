using Microsoft.AspNetCore.Mvc;
using Poc.App.Features.AwsOrganizations;

namespace Poc.Api.Endpoints;

public static class AwsOrganizationEndpoint
{
    public static RouteGroupBuilder MapAwsOrganizationEndpoint(this WebApplication app)
    {
        var group = app.MapGroup("/awsOrg").WithTags("awsOrganization");
        group.MapGet("/", GetAllOrganization);
        group.MapPost("/add", AddOrganization);
        group.MapPut("/update", UpdateOrganization);
        group.MapDelete("/delete", DeleteById);
        return group;
    }

    private static async Task<IResult> GetAllOrganization(IAwsOrg awsOrg)
    {
        return Results.Ok(await awsOrg.GetOrgAsync());
    }

    private static async Task<IResult> AddOrganization([FromBody] AwsOrgModel model, IAwsOrg awsOrg)
    {
        return Results.Ok(await awsOrg.AddOrgAsync(model));
    }

    private static async Task<IResult> UpdateOrganization([FromBody] AwsOrgModel model, IAwsOrg awsOrg)
    {
        return Results.Ok(await awsOrg.UpdateOrgAsync(model));
    }

    private static async Task<IResult> DeleteById([FromQuery] Guid id, IAwsOrg awsOrg)
    {
        return Results.Ok(await awsOrg.DeleteOrgAsync(id));
    }
}