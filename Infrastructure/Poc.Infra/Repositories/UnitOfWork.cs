using Poc.Domain.Repositories;
using Poc.Infra.Context;

namespace Poc.Infra.Repositories;

public class UnitOfWork(PocContext context) : IUnitOfWork
{
    private readonly PocContext _context = context;

    public bool SaveChange()
    {
        using var transaction = _context.Database.BeginTransaction();
        try
        {
            var result = _context.SaveChanges() > 0;
            transaction.Commit();
            return result;
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    public async Task<bool> SaveChangeAsync()
    {
        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var result = (await _context.SaveChangesAsync()) > 0;
            await transaction.CommitAsync();
            return result;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<IEnumerable<T>> SelectWithSqlRaw<T>(string sql)
    {
        var migrations = await _context.Database
            .SqlQueryRaw<T>(sql)
            .ToListAsync();
        return migrations;

    }
}
