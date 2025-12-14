using Amazon;
using Amazon.Organizations;
using Amazon.Organizations.Model;
using Amazon.Runtime;
using Amazon.SecurityToken;
using Amazon.SecurityToken.Model;
using Microsoft.Extensions.Options;
using Poc.App.Models;
using Poc.App.Options;
using Poc.App.Services;

namespace Poc.Infra.ThirdParties;

public class AWSService(IOptionsSnapshot<AwsConfigure> config) : IAwsService
{
    private readonly AwsConfigure _config = config.Value;

    public async Task<AccountResult> CreateAwsAccount(IdentityResult identityResult, string targetOU)
    {
        var accessKey = identityResult.AccessKeyId;
        var secretKey = identityResult.SecretKeyId;
        var awsSessionToken = identityResult.SessionToken;
        var randomAccount = Guid.CreateVersion7();
        var accountName = $"DevAccount-{randomAccount}";
        var email = $"DevTeamTech-{randomAccount}@myEmail.com";

        var client = new AmazonOrganizationsClient(accessKey, secretKey, awsSessionToken, RegionEndpoint.GetBySystemName(_config.DefaultRegion));
        var response = await client.CreateAccountAsync(new()
        {
            AccountName = accountName,
            Email = email
        });
        string requestId = response.CreateAccountStatus.Id;
        CreateAccountStatus status;
        do
        {
            await Task.Delay(10000);
            var describe = await client.DescribeCreateAccountStatusAsync(new()
            {
                CreateAccountRequestId = requestId
            });
            status = describe.CreateAccountStatus;

        } while (status.State == CreateAccountState.IN_PROGRESS);

        var roots = await client.ListRootsAsync(new());
        string rootId = roots.Roots.First().Id;

        await client.MoveAccountAsync(new MoveAccountRequest
        {
            AccountId = status.AccountId,
            SourceParentId = rootId,
            DestinationParentId = targetOU
        });
        var stsClient = new AmazonSecurityTokenServiceClient(accessKey, secretKey, awsSessionToken, RegionEndpoint.GetBySystemName(_config.DefaultRegion));
        await stsClient.AssumeRoleAsync(new AssumeRoleRequest
        {
            RoleArn = $"arn:aws:iam::{status.AccountId}:role/aws-test",
            RoleSessionName = $"AssumeSession-{Guid.CreateVersion7()}"
        });

        return new()
        {
            AccountId = status.AccountId,
            Name = accountName,
            Email = email,
            DestinationOU = targetOU,
            Status = status.State.Value
        };
    }

    public async Task<IdentityResult> SaveAuthenticationAsync(string? accessToken)
    {

        var awsClient = new AmazonSecurityTokenServiceClient(new AmazonSecurityTokenServiceConfig()
        {
            RegionEndpoint = RegionEndpoint.GetBySystemName(_config.DefaultRegion)
        });

        var request = new AssumeRoleWithWebIdentityRequest
        {
            RoleArn = _config.Role,
            WebIdentityToken = accessToken,
            RoleSessionName = $"Auth0M2M-{Guid.NewGuid()}",
        };
        var response = await awsClient.AssumeRoleWithWebIdentityAsync(request);
        return new()
        {
            AccessKeyId = response.Credentials.AccessKeyId,
            SecretKeyId = response.Credentials.SecretAccessKey,
            ExpirationDate = response.Credentials.Expiration,
            SessionToken = response.Credentials.SessionToken
        };
    }

    public async Task<IdentityResult> AssumeRoleAws(IdentityResult identityResult, string role, string type)
    {
        var session = new SessionAWSCredentials(identityResult.AccessKeyId, identityResult.SecretKeyId, identityResult.SessionToken);
        var awsClient = new AmazonSecurityTokenServiceClient(session, new AmazonSecurityTokenServiceConfig()
        {
            RegionEndpoint = RegionEndpoint.GetBySystemName(_config.DefaultRegion)
        });
        var response = await awsClient.AssumeRoleAsync(new AssumeRoleRequest
        {
            RoleArn = role,
            RoleSessionName = $"{type}-{Guid.NewGuid()}"
        });

        var credential = response.Credentials;

        return new()
        {
            AccessKeyId = credential.AccessKeyId,
            SessionToken = credential.SessionToken,
            SecretKeyId = credential.SecretAccessKey
        };
    }

    public async Task<IEnumerable<(string orgId, string orgName)>> ListOUAsync(IdentityResult identityResult)
    {
        var client = new AmazonOrganizationsClient(identityResult.AccessKeyId, identityResult.SecretKeyId, identityResult.SessionToken, RegionEndpoint.GetBySystemName(_config.DefaultRegion));
        var response = await client.ListRootsAsync(new ListRootsRequest());
        var result = new List<(string orgId, string orgName)>();
        var rootMetadata = response.Roots.First();
        var listOrgRequest = new ListOrganizationalUnitsForParentRequest()
        {
            ParentId = rootMetadata.Id
        };
        var data = await client.ListOrganizationalUnitsForParentAsync(listOrgRequest);
        foreach (var item in data.OrganizationalUnits)
        {
            result.Add((item.Id, item.Name));
        }

        return result;
    }
}