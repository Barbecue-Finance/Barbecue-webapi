using Infrastructure.BaseAbstractions;
using Models.Db.OperationCategories;

namespace Infrastructure.Abstractions
{
    using T = OutComeOperationCategory;

    public interface IOutComeOperationCategoryRepository : IAdd<T>, IUpdate<T>, IRemove<T>, IGetOne<T>, IGetMany<T>, IGetById<T>
    {
    }
}