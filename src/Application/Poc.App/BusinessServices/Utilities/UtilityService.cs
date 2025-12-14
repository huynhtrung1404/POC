using Poc.App.Commons;
using Poc.Domain.Repositories;

namespace Poc.App.BusinessServices.Utilities;

public class UtilityService(IUnitOfWork unitOfWork) : IUtilityService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public string DecodeBase64(string input) =>
     Helper.Base64Decode(input);

    public string EncodeBase64(string input) =>
         Helper.Base64Encode(input);


    public async Task<IEnumerable<MigrationLogDto>> GetMigrationLogAsync()
    {
        var data = await _unitOfWork.SelectWithSqlRaw<MigrationLogDto>("SELECT MigrationId, ProductVersion FROM [__EFMigrationsHistory]");
        return data;
    }

    public IEnumerable<string> ListGuid(int quantity = 1)
    {
        if (quantity > 50 || quantity <= 0)
            quantity = 1;
        for (var i = 0; i < quantity; i++)
        {
            yield return Guid.NewGuid().ToString();
        }

    }
}