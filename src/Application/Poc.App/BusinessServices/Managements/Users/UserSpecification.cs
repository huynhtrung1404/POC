using Poc.App.Specifications;
using Poc.Domain.Entities;

namespace Poc.App.BusinessServices.Managements.Users;

public class UserSpecification : BaseSpecification<User>
{
    public UserSpecification(int pageSize, int pageNumber) : base(default!)
    {
        ApplyPaging(pageNumber, pageSize);
        AddTrackingStatus(true);
    }

    public UserSpecification(Guid id) : base(x => x.Id == id)
    {
    }

    public UserSpecification() : base(default!)
    {

    }
}