
using Poc.Domain.Entities;
using Poc.Domain.Repositories;

namespace Poc.App.Features.AwsOrganizations;

public class AwsOrg(IRepository<AwsOrganization> awsOrgRepository, IUnitOfWork unitOfWork) : IAwsOrg
{
    private readonly IRepository<AwsOrganization> _awsOrganization = awsOrgRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<bool> AddOrgAsync(AwsOrgModel orgModel)
    {
        var awsOrganization = new AwsOrganization
        {
            Name = orgModel.OrgName,
            OrgId = orgModel.OrgId
        };
        await _awsOrganization.AddAsync(awsOrganization);
        return await _unitOfWork.SaveChangeAsync();
    }

    public async Task<bool> DeleteOrgAsync(Guid id)
    {
        await _awsOrganization.DeleteAsync(id);
        return await _unitOfWork.SaveChangeAsync();
    }

    public async Task<List<AwsOrgModel>> GetOrgAsync()
    {
        var result = await _awsOrganization.GetAllAsync();
        return [.. result.Select(x => new AwsOrgModel
        {
            Id = x.Id.ToString(),
            OrgName = x.Name,
            OrgId = x.OrgId
        })];
    }

    public async Task<bool> UpdateOrgAsync(AwsOrgModel orgModel)
    {
        var awsOrganization = new AwsOrganization
        {
            Name = orgModel.OrgName,
            OrgId = orgModel.OrgId
        };
        _awsOrganization.Update(awsOrganization);
        return await _unitOfWork.SaveChangeAsync();
    }
}