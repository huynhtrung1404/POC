using Poc.Domain.Entities;
using Poc.Domain.Repositories;

namespace Poc.App.BusinessServices.AwsOrganizations;

public class AwsOrganizationService(IRepository<AwsOrganization> orgRepository, IUnitOfWork unitOfWork) : IAwsOrganizationService
{
    private readonly IRepository<AwsOrganization> _orgRepository = orgRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<bool> AddAsync(AwsOrgDto awsOrgDto)
    {
        var result = new AwsOrganization
        {
            Name = awsOrgDto.Name,
            IsDeleted = false,
            ManagementAccount = awsOrgDto.AccountId,
            OrgId = awsOrgDto.OrgId
        };
        await _orgRepository.AddAsync(result);
        return await _unitOfWork.SaveChangeAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        await _orgRepository.DeleteAsync(id);
        await _unitOfWork.SaveChangeAsync();
    }

    public async Task<AwsOrgDto> GetDetailAsync(Guid id)
    {
        var data = await _orgRepository.GetItemAsync(new AwsOrganizationSpecification(id)) ?? throw new Exception("Data not found");
        return new()
        {
            AccountId = data.ManagementAccount,
            Id = data.Id,
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
            AccountId = x.ManagementAccount,
            Id = x.Id,
        });
    }

    public async Task<bool> UpdateAsync(AwsOrgDto awsOrgDto)
    {
        _orgRepository.Update(new AwsOrganization
        {
            Id = awsOrgDto.Id,
            Name = awsOrgDto.Name,
            OrgId = awsOrgDto.OrgId,
            IsDeleted = awsOrgDto.IsDeleted
        });
        return await _unitOfWork.SaveChangeAsync();
    }
}