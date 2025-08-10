using Poc.App.Features.AwsAccounts;

namespace Poc.Api.Endpoints;

public static class AwsAccountEndpoint
{
    public static RouteGroupBuilder MapAwsAccountEndpoint(this WebApplication app)
    {
        var group = app.MapGroup("/awsAccount").WithTags("AWS Account");
        group.MapGet("/", CreateAccount);
        return group;
    }

    private static async Task<IResult> CreateAccount(IAwsAccountService _awsAccountService)
    {
        return Results.Ok(await _awsAccountService.CreateAnAccount());
    }
}