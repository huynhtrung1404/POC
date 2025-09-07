using Poc.Domain.Repositories;

namespace Poc.App.BusinessServices.AwsOrganizations;

public class AwsOrganization(IRepository<AwsOrganization> orgRepository, IUnitOfWork unitOfWork) : IAwsOrganization
{
    private readonly IRepository<AwsOrganization> _orgRepository = orgRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public Task<AwsOrgDto> GetDetailAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<List<AwsOrgDto>> GetListAsync()
    {
        throw new NotImplementedException();
    }

}