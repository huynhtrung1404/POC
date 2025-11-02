namespace Poc.App.BusinessServices.Managements.AuthConfigs;

public class AuthConfigService(IRepository<AuthConfig> authConfigRepository, IUnitOfWork unitOfWork) : IAuthConfigService
{
    private readonly IRepository<AuthConfig> _authConfigRepository = authConfigRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public Task<bool> AddAsync(AuthConfigDto config)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<AuthConfigDto>> GetAllAsync(int pageSize, int pageNumber)
    {
        var data = await _authConfigRepository.GetAllAsync(new AuthConfigSpecification(pageSize, pageNumber));
        var result = data.Select(x => new AuthConfigDto()
        {
            Id = x.Id,
            Name = x.Name,
            ProviderName = x.ProviderName,
            ClientId = x.ClientId,
            ClientSecret = x.ClientSecret,
            RedirectUri = x.RedirectUri,
            Scopes = x.Scopes,
            IsActive = x.IsActive,
            Description = x.Description,
        });
        return result;
    }

    public async Task<AuthConfigDto> GetDetailItem(Guid id)
    {
        var data = await _authConfigRepository.GetAllAsync(new AuthConfigSpecification(id));
        var result = data.Select(x => new AuthConfigDto()
        {
            Id = x.Id,
            Name = x.Name,
            ProviderName = x.ProviderName,
            ClientId = x.ClientId,
            ClientSecret = x.ClientSecret,
            RedirectUri = x.RedirectUri,
            Scopes = x.Scopes,
            IsActive = x.IsActive,
            Description = x.Description,
        });
        return result.FirstOrDefault() ?? default!;
    }

    public Task<bool> UpdateAsync(AuthConfigDto config)
    {
        throw new NotImplementedException();
    }
}