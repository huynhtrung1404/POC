using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using Poc.App.Specifications;
using Poc.Domain.Entities;

namespace Poc.App.BusinessServices.Authentications;

public class UserSpecification : BaseSpecification<User>
{
    public UserSpecification(Guid id) : base(x => x.Id == id)
    {

    }

    public UserSpecification(string userName, string password, bool isNoTracking) : base(x => x.UserName == userName && x.Password == password)
    {
        AddTrackingStatus(isNoTracking);
    }
}