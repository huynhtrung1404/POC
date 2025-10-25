using Poc.App.Models;
using Poc.Domain.Entities;
using Poc.Domain.Repositories;

namespace Poc.App.BusinessServices.Managements.Tokens;

public class ManageTokenService(IRepository<Token> tokenRepository, IUnitOfWork unitOfWork) : IManageTokenService
{
    private readonly IRepository<Token> _tokenRepository = tokenRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<bool> DeleteAsync(Guid id)
    {
        await _tokenRepository.DeleteAsync(id);
        return await _unitOfWork.SaveChangeAsync();
    }

    public async Task<PagingResponse<TokenDto>> GetAllAsync(int pageSize, int pageNumber)
    {
        var data = await _tokenRepository.GetAllAsync(new ManageTokenSpecification(pageSize, pageNumber));
        var items = data.Select(x => new TokenDto
        {
            AccessToken = x.AccessToken,
            RefreshToken = x.RefreshToken,
            CreatedBy = x.CreatedBy,
            UpdatedBy = x.UpdatedBy,
            ExpireDate = x.ExpireTime,
            IsDeleted = x.IsDeleted
        });
        var countData = await _tokenRepository.CountAsync(new ManageTokenSpecification());
        return new()
        {
            Result = items,
            PageNumber = pageNumber,
            PageSize = pageSize,
            Total = countData
        };
    }

    public async Task<TokenDto> GetDetailAsync(Guid id)
    {
        var data = await _tokenRepository.GetItemAsync(new ManageTokenSpecification(id));
        return new TokenDto
        {
            AccessToken = data?.AccessToken,
            RefreshToken = data?.RefreshToken,
            CreatedBy = data?.CreatedBy,
            UpdatedBy = data?.UpdatedBy,
            ExpireDate = data?.ExpireTime,
            IsDeleted = data!.IsDeleted
        };
    }
}