namespace Poc.Domain.Repositories;

public interface IUnitOfWork
{
    bool SaveChange();
    Task<bool> SaveChangeAsync();
}