using Poc.Domain.Specifications;
using Poc.Infra.Context;
using Poc.Infra.Specifications;

namespace Poc.Infra.Repositories;

public class GenericRepository<T>(PocContext context) : IGenericRepository<T> where T : class
{
    protected DbSet<T> _dbSet = context.Set<T>();

    public void Add(T entity)
    {
        _dbSet.Add(entity);
    }

    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public async Task DeleteAsync(Guid id)
    {
        var data = await _dbSet.FindAsync(id);
        if (data is null)
        {
            throw new Exception("Data is not found");
        }
        _dbSet.Remove(data);
    }

    public IEnumerable<T> GetAll()
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<IEnumerable<T>> GetAllAsync(ISpecification<T> specification)
    {
        return await DbEvaluator.GetQuery(_dbSet, specification).ToListAsync();
    }

    public T GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<T?> GetItemAsync(ISpecification<T> specification)
    {
        return await DbEvaluator.GetQuery<T>(_dbSet, specification).FirstOrDefaultAsync();
    }

    public void Update(T entity)
    {
        _dbSet.Update(entity);
    }

    public async Task<int> CountAsync(ISpecification<T> specification)
    {
        return await DbEvaluator.CountValue<T>(_dbSet, specification);
    }
}