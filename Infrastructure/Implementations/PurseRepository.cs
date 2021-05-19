using Infrastructure.Abstractions;
using Infrastructure.BaseImplementations;
using Models.Db;

namespace Infrastructure.Implementations
{
    public class PurseRepository : IdRepositoryBase<Purse>, IPurseRepository
    {
        public PurseRepository(BarbecueDbContext context) : base(context)
        {
        }
    }
}