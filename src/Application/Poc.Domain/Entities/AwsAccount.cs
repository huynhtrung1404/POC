namespace Poc.Domain.Entities;

public class AwsAccount : BaseEntity
{
    public string? AccountId { get; set; }
    public string? Email { get; set; }
    public string? OrganizationName { get; set; }
    public Guid AwsOrganizationId { get; set; }
    public AwsOrganization? AwsOrganization { get; set; }
    public string? Status { get; set; }
    public string? AccountName { get; set; }

}