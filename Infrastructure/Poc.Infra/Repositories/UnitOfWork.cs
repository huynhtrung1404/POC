using Poc.Domain.Repositories;
using Poc.Infra.Context;

namespace Poc.Infra.Repositories;

public class UnitOfWork(PocContext context) : IUnitOfWork
{
    private readonly PocContext _context = context;

    public bool SaveChange()
    {
        return _context.SaveChanges() > 0;
    }

    public async Task<bool> SaveChangeAsync()
    {
        return (await _context.SaveChangesAsync()) > 0;
    }
}
