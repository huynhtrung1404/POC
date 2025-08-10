namespace Poc.App.Features.AwsOrganizations;

public interface IAwsOrg
{
    Task<List<AwsOrgModel>> GetOrgAsync();
    Task<bool> AddOrgAsync(AwsOrgModel orgModel);
    Task<bool> UpdateOrgAsync(AwsOrgModel orgModel);
    Task<bool> DeleteOrgAsync(Guid id);
}