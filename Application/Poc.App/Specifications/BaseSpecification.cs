using System.Linq.Expressions;
using Poc.Domain.Specifications;

namespace Poc.App.Specifications;

public abstract class BaseSpecification<T>(Expression<Func<T, bool>> criteria) : ISpecification<T>
{
    public Expression<Func<T, bool>> Criteria { get; } = criteria;

    public List<Expression<Func<T, object>>> Includes { get; } = [];

    public List<string> IncludeString { get; } = [];

    public long Skip { get; private set; }

    public long Take { get; private set; }

    public bool IsPagingEnabled { get; private set; } = false;

    public bool IsNoTracking { get; private set; }

    protected virtual void AddInclude(Expression<Func<T, object>> include)
    {
        Includes.Add(include);
    }
    protected virtual void AddIncludeString(string includeString)
    {
        IncludeString.Add(includeString);
    }
    protected void ApplyPaging(int skip, int take)
    {
        Skip = skip;
        Take = take;
        IsPagingEnabled = true;
    }

    protected void AddTrackingStatus(bool isNoTracking)
    {
        IsNoTracking = isNoTracking;
    }
}