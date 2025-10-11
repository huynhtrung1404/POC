
using Poc.Domain.Repositories;

namespace Poc.App.BusinessServices.Utilities;

public class UtilityService(IUnitOfWork unitOfWork) : IUtilityService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<IEnumerable<MigrationLogDto>> GetMigrationLogAsync()
    {
        var data = await _unitOfWork.SelectWithSqlRaw<MigrationLogDto>("SELECT MigrationId, ProductVersion FROM [__EFMigrationsHistory]");
        return data;
    }
}