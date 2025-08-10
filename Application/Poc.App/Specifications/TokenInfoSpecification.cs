using Poc.Domain.Entities;

namespace Poc.App.Specifications;

public class TokenInfoSpecification : BaseSpecification<TokenInfo>
{
    public TokenInfoSpecification() : base(x => DateTime.Now < x.ExpireDate)
    {

    }
}