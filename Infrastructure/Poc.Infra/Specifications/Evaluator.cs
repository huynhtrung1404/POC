using Poc.Domain.Specifications;

namespace Poc.Infra.Specifications;

public class Evaluator
{
    public static IQueryable<T> GetQuery<T>(
       IQueryable<T> inputQuery,
       ISpecification<T> specification)
       where T : class
    {
        var query = inputQuery;

        // Filter first
        if (specification.Criteria != null)
        {
            query = query.Where(specification.Criteria);
        }

        // Includes
        query = specification.Includes
            .Aggregate(query, (current, include) => current.Include(include));

        // Include strings
        query = specification.IncludeString
            .Aggregate(query, (current, include) => current.Include(include));

        // Apply paging if enabled
        if (specification.IsPagingEnabled)
        {
            query = query.Skip((int)specification.Skip).Take((int)specification.Take);
        }
        if (specification.IsNoTracking)
        {
            query = query.AsNoTracking();
        }

        return query;
    }

}