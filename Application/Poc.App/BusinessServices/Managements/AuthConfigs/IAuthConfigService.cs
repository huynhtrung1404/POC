namespace Poc.App.BusinessServices.Managements.AuthConfigs;

public interface IAuthConfigService
{
    Task<IEnumerable<AuthConfigDto>> GetAllAsync(int pageSize, int pageNumber);
    Task<AuthConfigDto> GetDetailItem(Guid id);
    Task<bool> AddAsync(AuthConfigDto config);
    Task<bool> UpdateAsync(AuthConfigDto config);
    Task<bool> DeleteAsync(Guid id);
}