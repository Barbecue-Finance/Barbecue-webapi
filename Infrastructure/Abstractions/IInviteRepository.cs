using Infrastructure.BaseAbstractions;
using Models.Db;

namespace Infrastructure.Abstractions
{
    using T = Invite;

    public interface IInviteRepository : IAdd<T>, IUpdate<T>, IGetOne<T>, IGetMany<T>, IGetById<T>
    {
    }
}