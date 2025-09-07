using Poc.Domain.Entities;
using Poc.Domain.Repositories;

namespace Poc.App.BusinessServices.AwsOrganizations;

public class AwsOrganizationService(IRepository<AwsOrganization> orgRepository, IUnitOfWork unitOfWork) : IAwsOrganizationService
{
    private readonly IRepository<AwsOrganization> _orgRepository = orgRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<AwsOrgDto> GetDetailAsync(Guid id)
    {
        var data = await _orgRepository.GetItemAsync(new AwsOrganizationSpecification(id)) ?? throw new Exception("Data not found");
        return new()
        {
            Name = data.Name,
            OrgId = data.OrgId,
        };
    }

    public async Task<IEnumerable<AwsOrgDto>> GetListAsync()
    {
        var data = await _orgRepository.GetAllAsync();
        return data.Select(x => new AwsOrgDto
        {
            Name = x.Name,
            OrgId = x.OrgId,
        });
    }

}