namespace Poc.App.BusinessServices.AwsOrganizations;

public class AwsOrgDto
{
    public string? Name { get; set; }
    public string? OrgId { get; set; }
    public bool IsDeleted { get; set; }
    public string? AccountId { get; set; }
}