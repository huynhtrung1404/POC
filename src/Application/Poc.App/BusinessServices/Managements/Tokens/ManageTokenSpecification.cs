using Poc.App.Specifications;
using Poc.Domain.Entities;

namespace Poc.App.BusinessServices.Managements.Tokens;

public class ManageTokenSpecification : BaseSpecification<Token>
{
    public ManageTokenSpecification(int pageSize, int pageNumber) : base(x => !x.IsDeleted)
    {
        ApplyPaging(pageNumber, pageSize);
    }

    public ManageTokenSpecification() : base(x => !x.IsDeleted)
    {

    }

    public ManageTokenSpecification(Guid id) : base(x => x.Id == id)
    {

    }
}