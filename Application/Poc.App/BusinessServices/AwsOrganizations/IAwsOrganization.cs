namespace Poc.App.BusinessServices.AwsOrganizations;

public interface IAwsOrganization
{
    Task<List<AwsOrgDto>> GetListAsync();
    Task<AwsOrgDto> GetDetailAsync(Guid id);
}