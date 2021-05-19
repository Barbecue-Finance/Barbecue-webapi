using Infrastructure.BaseAbstractions;
using Models.Db.MoneyOperations;

namespace Infrastructure.Abstractions
{
    using T = IncomeMoneyOperation;

    public interface IIncomeMoneyOperationRepository : IAdd<T>, IGetById<T>, IGetOne<T>, IGetMany<T>, IUpdate<T>, IRemove<T>
    {
    }
}