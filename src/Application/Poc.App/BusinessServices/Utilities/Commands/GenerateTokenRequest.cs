namespace Poc.App.BusinessServices.Utilities.Commands;

public record GenerateTokenRequest(JwtConfigDto Config) : IRequest<string>;

public class Handler : IRequestHandler<GenerateTokenRequest, string>
{
    public Task<string> HandleAsync(GenerateTokenRequest request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}