using Poc.App.Models;

namespace Poc.App.BusinessServices.Managements.Users;

public interface IUserManageService
{
    Task<PagingResponse<UserDto>> GetListAsync(int pageSize, int pageNumber);
    Task<UserDto> GetDetailAsync(Guid id);
    Task<bool> AddAsync(UserDto userDto);
    Task<bool> UpdateAsync(Guid id, UserDto userDto);
    Task DeleteAsync(Guid id);
}