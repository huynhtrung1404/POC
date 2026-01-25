using Poc.Domain.Enums;

namespace Poc.App.BusinessServices.Utilities;

public class M2MAuthenticationSpecification : BaseSpecification<M2MAuthentication>
{
    public M2MAuthenticationSpecification(LoginType type) : base(x => x.LoginType == type)
    {

    }
}