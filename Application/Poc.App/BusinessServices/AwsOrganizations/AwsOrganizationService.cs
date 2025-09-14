using Poc.App.Services;
using Poc.Domain.Entities;
using Poc.Domain.Repositories;

namespace Poc.App.BusinessServices.AwsOrganizations;

public class AwsOrganizationService(IRepository<AwsOrganization> orgRepository,
    IUnitOfWork unitOfWork,
    IAwsService awsService,
    IAuth0Service auth0Service) : IAwsOrganizationService
{
    private readonly IRepository<AwsOrganization> _orgRepository = orgRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IAwsService _awsService = awsService;
    private readonly IAuth0Service _auth0Service = auth0Service;

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

    public async Task<IEnumerable<(string id, string name)>> GetListByAwsPortal()
    {
        var auth0 = await _auth0Service.GetAccessTokenAsync();
        var identityResult = await _awsService.SaveAuthenticationAsync(auth0.AccessToken);
        var listOU = await _awsService.ListOUAsync(identityResult);
        return listOU;
    }

}