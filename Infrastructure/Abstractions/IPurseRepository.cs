using Infrastructure.BaseAbstractions;
using Models.Db;

namespace Infrastructure.Abstractions
{
    using T = Purse;

    public interface IPurseRepository : IAdd<T>, IGetOne<T>, IGetById<T>, IGetMany<T>
    {
    }
}