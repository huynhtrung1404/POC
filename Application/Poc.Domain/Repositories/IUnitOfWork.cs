namespace Poc.Domain.Repositories;

public interface IUnitOfWork
{
    bool SaveChange();
    Task<bool> SaveChangeAsync();

    /// <summary>
    /// Executes raw SQL query for internal migration verification purposes.
    /// WARNING: This method is intended for internal utility use only. Not recommended for production use.
    /// </summary>
    /// <typeparam name="T">The type to map the query results to (e.g. MigrationDto)</typeparam>
    /// <param name="sql">The raw SQL query string</param>
    /// <returns>A collection of type T representing the query results</returns>
    Task<IEnumerable<T>> SelectWithSqlRaw<T>(string sql);
}