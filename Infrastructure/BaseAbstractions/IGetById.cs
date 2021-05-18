using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure.BaseAbstractions
{
    public interface IGetById<T>
    {
        Task<T> GetById(long id, params Expression<Func<T, object>>[] includes);
    }
}