using System.Linq.Expressions;

namespace Fireworks.Application.common.extensions;

public static class QueryExtensions
{
    public static IQueryable<T> WhereIf<T>(
        this IQueryable<T> source,
        bool condition,
        Expression<Func<T, bool>> predicate)
        => condition ? source.Where(predicate) : source;
}