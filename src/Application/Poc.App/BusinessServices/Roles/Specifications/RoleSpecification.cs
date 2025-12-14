using System.Linq.Expressions;

namespace Poc.App.BusinessServices.Roles.Specifications;

public class RoleSpecification : BaseSpecification<Role>
{
    public RoleSpecification(Guid id) : base(x => x.Id == id)
    {
    }
}