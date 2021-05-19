using Infrastructure.BaseAbstractions;
using Models.Db;

namespace Infrastructure.Abstractions
{
    using T = Group;
    public interface IGroupRepository : IAdd<T>, IUpdate<T>, IGetById<T>, IGetMany<T>
    {
    }
}