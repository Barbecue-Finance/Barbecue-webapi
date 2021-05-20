using Infrastructure.BaseAbstractions;
using Models.Db.OperationCategories;

namespace Infrastructure.Abstractions
{
    using T = IncomeOperationCategory;

    public interface IIncomeOperationCategoryRepository : IAdd<T>, IUpdate<T>, IRemove<T>, IGetOne<T>, IGetMany<T>, IGetById<T>
    {
    }
}