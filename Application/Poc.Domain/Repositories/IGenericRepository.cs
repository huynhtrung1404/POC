using Poc.Domain.Specifications;

namespace Poc.Domain.Repositories;

public interface IGenericRepository<T>
{
    void Add(T entity);
    Task AddAsync(T entity);
    void Update(T entity);
    Task DeleteAsync(Guid id);
    T GetById(Guid id);
    Task<T?> GetByIdAsync(Guid id);
    IEnumerable<T> GetAll();
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> GetAllAsync(ISpecification<T> specification);
    Task<T?> GetItemAsync(ISpecification<T> specification);
}