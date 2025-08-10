
using Poc.App.Services;
using Poc.Domain.Entities;
using Poc.Domain.Repositories;

namespace Poc.App.Features.AwsAccounts;

public class AwsAccountService(IRepository<AwsOrganization> awsOrganizationRepository,
    IRepository<AwsAccount> awsAccountRepository,
    IAwsService awsService,
    ITokenService tokenService,
    IUnitOfWork unitOfWork) : IAwsAccountService
{
    private readonly IRepository<AwsOrganization> _awsOrganizationRepository = awsOrganizationRepository;

    private readonly IRepository<AwsAccount> _awsAccountRepository = awsAccountRepository;
    private readonly IAwsService _awsService = awsService;
    private readonly ITokenService _tokenService = tokenService;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<bool> CreateAnAccount()
    {
        var authenticationResult = await _tokenService.LoginAsync();
        var orgDetails = await _awsOrganizationRepository.GetAllAsync();
        var targetOu = orgDetails.FirstOrDefault()?.OrgId;
        if (string.IsNullOrWhiteSpace(targetOu))
            return false;
        var data = await _awsService.CreateAwsAccount(authenticationResult, targetOu);
        var awsAccount = new AwsAccount
        {
            AccountId = data.AccountId,
            AwsOrganizationId = orgDetails.FirstOrDefault()!.Id,
            OrganizationName = targetOu,
            Email = data.Email,
        };
        await _awsAccountRepository.AddAsync(awsAccount);
        return await _unitOfWork.SaveChangeAsync();
    }

    public Task<bool> CreateMultipleAccount()
    {
        throw new NotImplementedException();
    }
}