using Poc.App.Specifications;
using Poc.Domain.Entities;

namespace Poc.App.BusinessServices.AwsOrganizations;

public class AwsOrganizationSpecification : BaseSpecification<AwsOrganization>
{
    public AwsOrganizationSpecification(Guid id) : base(x => x.Id == id)
    {

    }
}