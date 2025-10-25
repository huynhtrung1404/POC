using Poc.App.Models;

namespace Poc.App.BusinessServices.Managements.Tokens;

public interface IManageTokenService
{
    Task<PagingResponse<TokenDto>> GetAllAsync(int pageSize, int pageNumber);
    Task<TokenDto> GetDetailAsync(Guid id);
    Task<bool> DeleteAsync(Guid id);
}