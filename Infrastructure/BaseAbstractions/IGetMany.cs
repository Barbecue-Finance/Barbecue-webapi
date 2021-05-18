using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure.BaseAbstractions
{
    public interface IGetMany<T>
    {
        Task<ICollection<T>> GetMany(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includes);
    }
}