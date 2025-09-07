namespace Poc.App.BusinessServices.AwsOrganizations;

public interface IAwsOrganizationService
{
    Task<IEnumerable<AwsOrgDto>> GetListAsync();
    Task<AwsOrgDto> GetDetailAsync(Guid id);
}