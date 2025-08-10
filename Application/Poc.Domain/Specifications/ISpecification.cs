using System.Linq.Expressions;

namespace Poc.Domain.Specifications;

public interface ISpecification<T>
{
    Expression<Func<T, bool>> Criteria { get; }
    List<Expression<Func<T, object>>> Includes { get; }
    List<string> IncludeString { get; }
    long Skip { get; }
    long Take { get; }
    bool IsPagingEnabled { get; }
}