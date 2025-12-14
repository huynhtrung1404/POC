namespace Poc.App.BusinessServices.Managements.AuthConfigs;

public class AuthConfigService(IRepository<AuthConfig> authConfigRepository, IUnitOfWork unitOfWork) : IAuthConfigService
{
    private readonly IRepository<AuthConfig> _authConfigRepository = authConfigRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<bool> AddAsync(AuthConfigDto config)
    {
        var data = new AuthConfig
        {
            Id = config.Id,
            Name = config.Name,
            ProviderName = config.ProviderName,
            ClientId = config.ClientId,
            ClientSecret = config.ClientSecret,
            RedirectUri = config.RedirectUri,
            Scopes = config.Scopes,
            IsActive = config.IsActive,
            Description = config.Description,
            AdditionParam = config.AdditionParam,
            Audience = config.Audience,
            Authority = config.Authority,
            Domain = config.Domain,
            PostLogoutRedirectUri = config.PostLogoutRedirectUri,
            TenantId = config.TenantId,
        };
        await _authConfigRepository.AddAsync(data);
        return await _unitOfWork.SaveChangeAsync();
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        await _authConfigRepository.DeleteAsync(id);
        return await _unitOfWork.SaveChangeAsync();
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
            AdditionParam = x.AdditionParam,
            Audience = x.Audience,
            Authority = x.Authority,
            Domain = x.Domain,
            PostLogoutRedirectUri = x.PostLogoutRedirectUri,
            TenantId = x.TenantId,
        });
        return result;
    }

    public async Task<AuthConfigDto> GetDetailItem(Guid id)
    {
        var data = await _authConfigRepository.GetItemAsync(new AuthConfigSpecification(id)) ??
            throw new Exception("Not found");
        var result = new AuthConfigDto()
        {
            Id = data.Id,
            Name = data.Name,
            ProviderName = data.ProviderName,
            ClientId = data.ClientId,
            ClientSecret = data.ClientSecret,
            RedirectUri = data.RedirectUri,
            Scopes = data.Scopes,
            IsActive = data.IsActive,
            Description = data.Description,
            AdditionParam = data.AdditionParam,
            Audience = data.Audience,
            Authority = data.Authority,
            Domain = data.Domain,
            PostLogoutRedirectUri = data.PostLogoutRedirectUri,
            TenantId = data.TenantId,
        };
        return result;
    }

    public async Task<bool> UpdateAsync(AuthConfigDto config)
    {
        var data = new AuthConfig
        {
            Id = config.Id,
            Name = config.Name,
            ProviderName = config.ProviderName,
            ClientId = config.ClientId,
            ClientSecret = config.ClientSecret,
            RedirectUri = config.RedirectUri,
            Scopes = config.Scopes,
            IsActive = config.IsActive,
            Description = config.Description,
            AdditionParam = config.AdditionParam,
            Audience = config.Audience,
            Authority = config.Authority,
            Domain = config.Domain,
            PostLogoutRedirectUri = config.PostLogoutRedirectUri,
            TenantId = config.TenantId,
        };
        _authConfigRepository.Update(data);
        return await _unitOfWork.SaveChangeAsync();
    }
}