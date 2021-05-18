using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models.Db;
using Models.Db.Common;

namespace Infrastructure.BaseImplementations
{
    public class IdRepositoryBase<T> : RepositoryBase<T> where T : IdEntity
    {
        protected IdRepositoryBase(BarbecueDbContext context) : base(context)
        {
        }

        public async Task Update(T entity)
        {
            GetDbSetT().Update(entity);
            await Context.SaveChangesAsync();
        }

        public async Task Remove(T entity)
        {
            GetDbSetT().Remove(entity);
            await Context.SaveChangesAsync();
        }

        public async Task RemoveMany(ICollection<T> entities)
        {
            GetDbSetT().RemoveRange(entities);
            await Context.SaveChangesAsync();
        }

        public async Task Add(T entity)
        {
            GetDbSetT().Add(entity);
            await Context.SaveChangesAsync();
        }

        public async Task<ICollection<T>> GetMany(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includes)
        {
            return await GetDbSetT().OrderBy(e => e.Id).AggregateIncludes(includes).ApplyPredicate(predicate).ToListAsync();
        }

        public async Task<T> GetById(long id, params Expression<Func<T, object>>[] includes)
        {
            return await GetDbSetT().AggregateIncludes(includes).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Dictionary<long, T>> GetAllAsDictionary()
        {
            return
                (await GetDbSetT().ToListAsync())
                .ToDictionary(entity => entity.Id, entity => entity);
        }
    }
}