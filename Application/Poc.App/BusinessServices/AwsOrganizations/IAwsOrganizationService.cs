namespace Poc.App.BusinessServices.AwsOrganizations;

public interface IAwsOrganizationService
{
    Task<IEnumerable<AwsOrgDto>> GetListAsync();
    Task<AwsOrgDto> GetDetailAsync(Guid id);
    Task<bool> AddAsync(AwsOrgDto awsOrgDto);
    Task<bool> UpdateAsync(AwsOrgDto awsOrgDto);
    Task DeleteAsync(Guid id);
}