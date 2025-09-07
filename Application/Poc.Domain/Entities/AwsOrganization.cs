namespace Poc.Domain.Entities;

public class AwsOrganization : BaseEntity
{
    public string? Name { get; set; }
    public string? OrgId { get; set; }
    public string? AccountId { get; set; }
    public string? IsDeleted { get; set; }
}