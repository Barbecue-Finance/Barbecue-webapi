using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> AggregateIncludes<T>(this IQueryable<T> queryable, Expression<Func<T, object>>[] includes) where T : class
        {
            return includes.Aggregate(queryable, (current, include) => current.Include(include));
        }

        public static IQueryable<T> ApplyPredicate<T>(this IQueryable<T> queryable, Expression<Func<T, bool>> predicate)
        {
            return predicate == null ? queryable : queryable.Where(predicate);
        }
    }
}