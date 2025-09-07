namespace Poc.Domain.Entities;

public class AwsOrganization : BaseEntity
{
    public string? Name { get; set; }
    public string? OrgId { get; set; }
    public bool? IsDeleted { get; set; }
    public string? ManagementAccount { get; set; }
}