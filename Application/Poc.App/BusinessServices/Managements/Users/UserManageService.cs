using Poc.App.Models;
using Poc.Domain.Entities;
using Poc.Domain.Repositories;

namespace Poc.App.BusinessServices.Managements.Users;

public class UserManageService(IRepository<User> userRepository, IUnitOfWork unitOfWork) : IUserManageService
{
    private readonly IRepository<User> _userRepository = userRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<bool> AddAsync(UserDto userDto)
    {
        var data = new User
        {
            UserName = userDto.UserName,
            Address = userDto.Address,
            Email = userDto.Email,
            Age = userDto.Age,
            IsDisabled = userDto.IsDisabled
        };
        await _userRepository.AddAsync(data);
        return await _unitOfWork.SaveChangeAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        await _userRepository.DeleteAsync(id);
        await _unitOfWork.SaveChangeAsync();
    }

    public async Task<UserDto> GetDetailAsync(Guid id)
    {
        var response = await _userRepository.GetItemAsync(new UserSpecification(id)) ?? throw new Exception("Not found item");
        return new UserDto
        {
            UserName = response.UserName,
            Email = response.Email,
            Address = response.Address,
            Age = response.Age,
            IsDisabled = response.IsDisabled
        };
    }

    public async Task<PagingResponse<UserDto>> GetListAsync(int pageSize, int pageNumber)
    {
        var specification = new UserSpecification(pageSize, pageNumber);
        var users = await _userRepository.GetAllAsync(specification);
        var result = users.Select(x => new UserDto
        {
            Address = x.Address,
            Age = x.Age,
            Email = x.Email,
            IsDisabled = x.IsDisabled,
            UserName = x.UserName
        });
        return new()
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            Result = result,
            Total = await _userRepository.CountAsync(new UserSpecification())
        };
    }


    public async Task<bool> UpdateAsync(Guid id, UserDto userDto)
    {
        var existingUser = await _userRepository.GetByIdAsync(id) ?? throw new Exception("No Data found");
        existingUser.Age = userDto.Age;
        existingUser.Email = userDto.Email;
        existingUser.Address = userDto.Address;
        return await _unitOfWork.SaveChangeAsync();
    }
}