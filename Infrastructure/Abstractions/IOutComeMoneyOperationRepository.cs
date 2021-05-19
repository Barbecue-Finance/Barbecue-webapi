using Infrastructure.BaseAbstractions;
using Models.Db.MoneyOperations;

namespace Infrastructure.Abstractions
{
    using T = OutComeMoneyOperation;

    public interface IOutComeMoneyOperationRepository : IAdd<T>, IGetById<T>, IGetOne<T>, IGetMany<T>, IUpdate<T>, IRemove<T>
    {
    }
}