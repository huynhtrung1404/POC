using Poc.App.Specifications;
using Poc.Domain.Entities;

namespace Poc.App.BusinessServices.Authentications;

public class TokenSpecification : BaseSpecification<Token>
{
    public TokenSpecification(string refreshToken) : base(x => x.RefreshToken == refreshToken && x.ExpireTime > DateTime.UtcNow)
    {

    }
}