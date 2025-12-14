namespace Poc.App.BusinessServices.Managements.AuthConfigs;

public class AuthConfigSpecification : BaseSpecification<AuthConfig>
{
    public AuthConfigSpecification(int pageSize, int pageNumber) : base(null!)
    {
        ApplyPaging(pageNumber, pageSize);
    }

    public AuthConfigSpecification(Guid id) : base(x => x.Id == id)
    {

    }
}