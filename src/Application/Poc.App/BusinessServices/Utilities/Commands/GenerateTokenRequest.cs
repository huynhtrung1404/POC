using Poc.App.Services;

namespace Poc.App.BusinessServices.Utilities.Commands;

public record GenerateTokenRequest() : IRequest<string>;

public class Handler(IM2MAuthenticationService m2mService) : IRequestHandler<GenerateTokenRequest, string>
{
    private readonly IM2MAuthenticationService _m2mService = m2mService;

    public async Task<string> HandleAsync(GenerateTokenRequest request, CancellationToken cancellationToken = default)
    {
        return await _m2mService.GetAccessToken(Domain.Enums.LoginType.AzureAd);
    }
}