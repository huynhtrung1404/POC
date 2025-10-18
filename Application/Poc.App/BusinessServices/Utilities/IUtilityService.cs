namespace Poc.App.BusinessServices.Utilities;

public interface IUtilityService
{
    Task<IEnumerable<MigrationLogDto>> GetMigrationLogAsync();

    IEnumerable<string> ListGuid(int quantity = 1);

    string DecodeBase64(string input);
    string EncodeBase64(string input);
}
