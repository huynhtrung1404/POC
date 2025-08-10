using Poc.App.Models;
using Poc.App.Services;
using Poc.App.Specifications;
using Poc.Domain.Entities;
using Poc.Domain.Repositories;

namespace Poc.App.BusinessServices;

public class TokenService(IAuth0Service auth0Service,
                        IAwsService awsService,
                        IRepository<TokenInfo> tokenInfo,
                        IUnitOfWork unitOfWork,
                        ICachingService cachingService) : ITokenService
{
    private readonly IAuth0Service _auth0Service = auth0Service;
    private readonly IAwsService _awsService = awsService;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IRepository<TokenInfo> _tokenInfoRepository = tokenInfo;
    private readonly ICachingService _cachingService = cachingService;


    public async Task<IdentityResult> LoginAsync()
    {
        var tokenDatabase = await _tokenInfoRepository.GetItemAsync(new TokenInfoSpecification());
        Auth0Result tokenDetail;
        if (tokenDatabase is null)
        {

            tokenDetail = await _auth0Service.GetAccessTokenAsync();
            var data = await _awsService.SaveAuthenticationAsync(tokenDetail.AccessToken);
            var model = new TokenInfo()
            {
                AccessToken = tokenDetail.AccessToken,
                ExpireDate = tokenDetail.ExpireTime ?? default,
                TokenType = tokenDetail.TokenType,
                AwsAccessKey = data.AccessKeyId,
                SessionToken = data.SessionToken
            };
            await _tokenInfoRepository.AddAsync(model);
            await _unitOfWork.SaveChangeAsync();
            var caching = _cachingService.GetOrCreate(model.AwsAccessKey, () => data, data.ExpirationDate?.TimeOfDay);
            return data;
        }
        var result = _cachingService.GetOrCreate<IdentityResult>(tokenDatabase?.AwsAccessKey!, () => new(), default);
        if (result is null || string.IsNullOrWhiteSpace(result.AccessKeyId))
        {
            result = await _awsService.SaveAuthenticationAsync(tokenDatabase?.AccessToken);
            tokenDatabase!.AwsAccessKey = result.AccessKeyId;
            var caching = _cachingService.GetOrCreate(result.AccessKeyId, () => result, result.ExpirationDate?.TimeOfDay);
        }
        return result;
    }
}