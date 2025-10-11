namespace Poc.App.BusinessServices.Utilities;

public interface IUtilityService
{
    Task<IEnumerable<MigrationLogDto>> GetMigrationLogAsync();
}